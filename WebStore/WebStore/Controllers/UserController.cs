using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.DTO;
using WebStore.Entity;
using WebStore.Service;
using WebStore.Service.IService;


namespace ApiWebQuanAo.Web.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService) // Dùng IUserService thay vì UserService
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _userService.RegisterAsync(request.Username, request.Email, request.Password);
            if (!result)
                return BadRequest("Username đã tồn tại.");

            return Ok("Đăng ký thành công.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _userService.LoginAsync(request.Email, request.Password);

            if (response == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            if (users == null || users.Count == 0)
                return NoContent(); // Trả về HTTP 204 nếu không có dữ liệu

            return Ok(users); // Trả về HTTP 200 với danh sách UserDto
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}