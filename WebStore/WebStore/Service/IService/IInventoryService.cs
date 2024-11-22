using WebStore.DTO;

namespace WebStore.Service.IService
{
    public interface IInventoryService
    {
        Task<List<InventoryDTO>> GetAllAsync();
        Task<InventoryDTO> GetByIdAsync(int id);
        Task AddAsync(InventoryDTO inventoryDto);
        Task UpdateAsync(InventoryDTO inventoryDto);
        Task DeleteByIdAsync(int id);
    }

}
