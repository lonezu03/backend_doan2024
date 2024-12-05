namespace WebStore.DTO
{
    public class UpdateOrderDto
    {
        public DateTime Date { get; set; } // Ngày đặt hàng
        public decimal TotalAmount { get; set; } // Tổng số tiền
        public string Status { get; set; } // Trạng thái (pending, completed, e
    }
}