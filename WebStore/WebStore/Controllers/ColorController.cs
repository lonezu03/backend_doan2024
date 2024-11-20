using Microsoft.AspNetCore.Mvc;
using WebStore.Service.IService;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddColor([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Name is required" });
            }

            var color = await _colorService.AddColorAsync(name);
            return Ok(color);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var result = await _colorService.DeleteColorAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Color not found" });
            }

            return Ok(new { message = "Color deleted successfully" });
        }
    }

}
