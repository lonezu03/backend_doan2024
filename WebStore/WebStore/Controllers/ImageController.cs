using Microsoft.AspNetCore.Mvc;
using WebStore.DTO;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
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

                // Chuyển byte[] sang chuỗi Base64
                string base64Image = Convert.ToBase64String(image.Url);

                // Loại nội dung, ví dụ: image/jpeg hoặc image/png
                string contentType = "image/jpeg"; // Hoặc lấy từ metadata nếu cần

                return Ok(new
                {
                    ImageData = base64Image,
                    ContentType = contentType
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImageDto imageDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _imageService.AddAsync(imageDto);
            return CreatedAtAction(nameof(GetById), new { id = imageDto.Id }, imageDto);
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
