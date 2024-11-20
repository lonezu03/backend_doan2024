namespace WebStore.DTO
{
    public class CreateOrderDto
    {
        public int User_Id { get; set; }
        public int Shipping_Id { get; set; }
        public DateTime Date { get; set; }
    }
}
