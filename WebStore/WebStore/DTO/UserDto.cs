namespace WebStore.DTO
{
    public class UserDto
    {
        public int Id { get; set; }         // Mã người dùng (Primary Key)

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone_number { get; set; }

    }
}
