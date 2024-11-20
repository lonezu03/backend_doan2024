using Microsoft.AspNetCore.Mvc;
using WebStore.DTO;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sizes = await _sizeService.GetAllAsync();
            return Ok(sizes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var size = await _sizeService.GetByIdAsync(id);
            if (size == null) return NotFound();
            return Ok(size);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SizeDto sizeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _sizeService.AddAsync(sizeDto);
            return CreatedAtAction(nameof(GetById), new { id = sizeDto.Id }, sizeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var size = await _sizeService.GetByIdAsync(id);
            if (size == null) return NotFound();

            await _sizeService.DeleteByIdAsync(id);
            return NoContent();
        }
    }

}
