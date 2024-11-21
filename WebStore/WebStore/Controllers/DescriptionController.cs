using Microsoft.AspNetCore.Mvc;
using WebStore.DTO;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/description")]
    [ApiController]
    public class DescriptionController : ControllerBase
    {
        private readonly IDescriptionService _descriptionService;

        public DescriptionController(IDescriptionService descriptionService)
        {
            _descriptionService = descriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var descriptions = await _descriptionService.GetAllAsync();
            return Ok(descriptions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var description = await _descriptionService.GetByIdAsync(id);
            if (description == null) return NotFound();
            return Ok(description);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DescriptionDto descriptionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _descriptionService.AddAsync(descriptionDto);
            return CreatedAtAction(nameof(GetById), new { id = descriptionDto.Id }, descriptionDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var description = await _descriptionService.GetByIdAsync(id);
            if (description == null) return NotFound();

            await _descriptionService.DeleteByIdAsync(id);
            return NoContent();
        }
    }

}
