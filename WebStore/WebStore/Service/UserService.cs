using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Reposiroty.Interface;
using WebStore.Service.IService;
using System.Security.Cryptography;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using WebStore.Repository;
using WebStore.DTO;
using AutoMapper;


namespace WebStore.Service
{


    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper; // Gán giá trị _mapper
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            // Sử dụng AutoMapper (nếu có)
            return _mapper.Map<List<UserDto>>(users);

        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            // Lấy người dùng theo email
            var user = await _userRepository.GetByEmailAsync(email);

            // Kiểm tra thông tin đăng nhập
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null; // Thông tin không hợp lệ
            }

            // Tạo token JWT
            var token = GenerateJwtToken(user);

            // Trả về thông tin người dùng kèm token
            return new LoginResponse
            {
                User = new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username
                },
                Token = token.Token,
                Expiration = token.Expiration
            };
        }

        private LoginResponse GenerateJwtToken(Users user)
        {
            var jwtConfig = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]);

            // Create claims
            var claims = new[]
            {
     new Claim(JwtRegisteredClaimNames.Sub, user.Email),

     new Claim("id", user.Id.ToString()),
     new Claim("name", user.Username),
     new Claim("created_at", DateTime.UtcNow.ToString("o")),

     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
     new Claim(ClaimTypes.Name, user.Email)
 };

            // Signing credentials
            var signingKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Token expiration
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtConfig["ExpiresInMinutes"]));

            var token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires
            };
        }
        public async Task<bool> UserExists(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null;
        }
        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            if (await UserExists(email))
                return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new Users
            {
                Email = email,
                Password = hashedPassword,
                Username = username
            };

            await _userRepository.AddAsync(user); // Gọi hàm thêm bất đồng bộ
            return true;
        }





        public int GetCurrentUserId()
        {
            // Lấy token từ header Authorization
            var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                throw new Exception("Token is missing or invalid.");
            }

            var token = authHeader.Substring("Bearer ".Length);

            // Giải mã token
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!jwtHandler.CanReadToken(token))
            {
                throw new Exception("Invalid token format.");
            }

            var jwtToken = jwtHandler.ReadJwtToken(token);

            // Lấy user_id từ claim
            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id");
            if (userIdClaim == null)
            {
                throw new Exception("User ID not found in token.");
            }

            // Trả về user_id
            return int.Parse(userIdClaim.Value);
        }
    }
    }