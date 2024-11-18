using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet("secured")]
        [Authorize]
        public IActionResult Secured()
        {
            var username = User.Identity.Name;
            return Ok(new { message = $"Hello {username}, you are authorized!" });
        }
    }
}
