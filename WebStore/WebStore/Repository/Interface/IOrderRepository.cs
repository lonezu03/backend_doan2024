using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<List<Orders>> GetAllOrdersAsync();       // Lấy tất cả các đơn hàng
        Task<Orders> GetOrderByIdAsync(int id);       // Lấy chi tiết đơn hàng theo ID
        Task<Orders> CreateOrderAsync(Orders order);   // Tạo mới đơn hàng
        Task<bool> UpdateOrderAsync(Orders order);    // Cập nhật đơn hàng
        Task<bool> DeleteOrderAsync(int id);
    }
}
