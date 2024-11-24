namespace WebStore.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Shipping_Id { get; set; }
        public DateTime Date { get; set; }

        public string? UserName { get; set; }
        public string? ShippingAddress { get; set; }
    }
}
