using Microsoft.AspNetCore.Mvc;
using WebStore.DTO;
using WebStore.Entity;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/variant")]
    [ApiController]
    public class VariantController : ControllerBase
    {
        private readonly IVariantService _variantService;

        public VariantController(IVariantService variantService)
        {
            _variantService = variantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var variants = await _variantService.GetAllAsync();
            return Ok(variants);
        }
        // PUT: api/Variant/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVariant(int id, [FromBody] VariantDto variantDto)
        {
            if (id != variantDto.Id)
            {
                return BadRequest("Variant ID mismatch");
            }

            await _variantService.UpdateVariantAsync(variantDto);
            return Ok(new { Message = "Variant updated successfully", Variant = variantDto });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var variant = await _variantService.GetByIdAsync(id);
            if (variant == null)
            {
                return NotFound(new { Message = "Variant not found" });
            }

            return Ok(variant);
        }
        [HttpDelete("deleteByProduct/{productId}")]
        public async Task<IActionResult> DeleteVariantsByProduct(int productId)
        {
            await _variantService.DeleteByProductIdAsync(productId);
            return NoContent(); // Trả về 204 nếu thành công
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] VariantDto variant)
        {
            
            await _variantService.AddAsync(variant);
            return CreatedAtAction(nameof(GetById), new { id = variant.Id }, variant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _variantService.DeleteByIdAsync(id);
            return NoContent();
        }
    }

}
