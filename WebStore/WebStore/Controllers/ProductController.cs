using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.DTO;
using WebStore.Entity;
using WebStore.Service.IService;
using WebStore.Controllers;


namespace ApiWebQuanAo.Web.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly Cloudinary _cloudinary;

        public ProductController(IProductService productService, Cloudinary cloudinary, ILogger<ProductController> logger)
        {
            _productService = productService;
            _cloudinary = cloudinary;
            _logger = logger;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto productDto, IFormFile imageFile)
        {
            try
            {
                // Kiểm tra tên sản phẩm có trùng lặp không
                var existingProduct = await _productService.GetProductByNameAsync(productDto.Name);
                if (existingProduct != null)
                {
                    return BadRequest(new { Message = "Product name already exists." });
                }

                // Tiếp tục xử lý logic tải ảnh và tạo sản phẩm như trước
                string imageUrl = null;
                if (imageFile != null && imageFile.Length > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                        return BadRequest("Unsupported file type");

                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.Error != null)
                    {
                        _logger.LogError("Cloudinary upload error: {ErrorMessage}", uploadResult.Error.Message);
                        return BadRequest($"Cloudinary upload failed: {uploadResult.Error.Message}");
                    }

                    imageUrl = uploadResult.SecureUrl.AbsoluteUri;
                }

                var createdProduct = await _productService.CreateProductAsync(productDto, imageUrl);
                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating product: {Error}", ex.Message);
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto productDto, IFormFile? imageFile)
        {
            try
            {
                // Kiểm tra sản phẩm
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                    return NotFound("Product not found");

                // Khởi tạo URL ảnh bằng ảnh hiện có (giữ nguyên nếu không upload ảnh mới)
                string imageUrl = product.Image;

                // Nếu ảnh được gửi từ frontend
                if (imageFile != null && imageFile.Length > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                        return BadRequest("Unsupported file type");

                    // Upload ảnh lên Cloudinary
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.Error != null)
                    {
                        _logger.LogError("Cloudinary upload error: {ErrorMessage}", uploadResult.Error.Message);
                        return BadRequest($"Cloudinary upload failed: {uploadResult.Error.Message}");
                    }

                    // Cập nhật URL ảnh mới nếu upload thành công
                    imageUrl = uploadResult.SecureUrl.AbsoluteUri;
                }

                // Cập nhật sản phẩm với dữ liệu mới từ frontend
                await _productService.UpdateProductAsync(id, productDto, imageUrl);

                // Trả về thông báo thành công
                return Ok(new { Message = "Product updated successfully", ProductId = id });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi và trả về thông báo lỗi
                _logger.LogError("Error updating product: {Error}", ex.Message);
                return StatusCode(500, new { Message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
        [HttpGet("GetAllWithVariants")]
        public async Task<IActionResult> GetAllWithVariants()
        {
            try
            {
                var products = await _productService.GetAllProductsWithVariantsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }

}

