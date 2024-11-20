using WebStore.DTO;

namespace WebStore.Service.IService
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync();              // Lấy danh sách đơn hàng (kèm thông tin người dùng & shipping)
        Task<OrderDto> GetOrderByIdAsync(int id);              // Lấy chi tiết đơn hàng theo ID
        Task<OrderDto> CreateOrderAsync(CreateOrderDto order); // Tạo mới đơn hàng
        Task<bool> UpdateOrderAsync(int id, UpdateOrderDto order); // Cập nhật đơn hàng
        Task<bool> DeleteOrderAsync(int id);                   // Xóa đơn hàng
    }
}
