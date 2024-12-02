using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DTO;
using WebStore.Service.IService;
using WebStore.Context;
using WebStore.Entity;

namespace WebStore.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ImageController> _logger;
        private readonly Cloudinary _cloudinary;


        public ImageController(IImageService imageService, Cloudinary cloudinary, ApplicationDbContext dbContext, ILogger<ImageController> logger)
        {
            _imageService = imageService;
            _cloudinary = cloudinary;
            _dbContext = dbContext;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var images = await _imageService.GetAllAsync();
            return Ok(images);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                // Lấy ảnh từ database
                var image = await _imageService.GetByIdAsync(id);

                if (image == null || image.Url == null)
                {
                    return NotFound(new { Message = "Image not found" });
                }

              

                return Ok(new
                {
                    ImageUrl = image.Url

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }


        [HttpGet("/id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null) return NotFound();
            return Ok(image);
        }



        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file, int? variantId)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded or file is empty");

                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                    return BadRequest("Unsupported file type");

                // Tải lên Cloudinary
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    _logger.LogError("Cloudinary upload error: {ErrorMessage}", uploadResult.Error.Message);
                    return BadRequest($"Cloudinary upload failed: {uploadResult.Error.Message}");
                }

                // Lưu thông tin vào database
                if (string.IsNullOrEmpty(uploadResult.SecureUrl?.AbsoluteUri))
                    return BadRequest("Invalid image URL from Cloudinary");

                var image = new Image
                {
                    Url = uploadResult.SecureUrl.AbsoluteUri,
                    Variant_Id = variantId
                };

                _dbContext.Image.Add(image);
                await _dbContext.SaveChangesAsync();

                return Ok(new
                {
                    Message = "Image uploaded successfully",
                    ImageId = image.Id,
                    ImageUrl = image.Url
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
            // Kiểm tra tệp
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null) return NotFound();

            await _imageService.DeleteByIdAsync(id);
            return NoContent();
        }
    }

}
