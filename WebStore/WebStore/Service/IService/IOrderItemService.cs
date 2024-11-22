using WebStore.DTO;
using WebStore.Entity;

namespace WebStore.Service.IService
{
    public interface IOrderItemService
    {
        Task<List<OrderItemDTO>> GetAllAsync();
        Task<OrderItemDTO> GetByIdAsync(int id);
        Task AddAsync(OrderItemDTO orderItemDto);
        Task UpdateAsync(OrderItemDTO orderItemDto);
        Task DeleteByIdAsync(int id);
    }

}
