namespace WebStore.DTO
{
    public class LoginResponse
    {
        public UserResponse User { get; set; } // Thông tin người dùng
        public string Token { get; set; } // Token JWT
        public DateTime Expiration { get; set; } // Ngày hết hạn của token
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
