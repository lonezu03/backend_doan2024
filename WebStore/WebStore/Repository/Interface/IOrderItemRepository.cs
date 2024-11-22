using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface IOrderItemRepository
    {
        Task<List<Order_Item>> GetAllAsync();
        Task<Order_Item> GetByIdAsync(int id);
        Task AddAsync(Order_Item orderItem);
        Task<Order_Item> UpdateAsync(Order_Item orderItem);

        Task DeleteByIdAsync(int id);
        Task SaveChangesAsync();
    }

}
