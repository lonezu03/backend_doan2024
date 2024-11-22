using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface IInventoryRepository
    {
        Task<List<Inventory>> GetAllAsync();
        Task<Inventory> GetByIdAsync(int id);
        Task AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
        Task DeleteByIdAsync(int id);
        Task SaveChangesAsync();
    }

}
