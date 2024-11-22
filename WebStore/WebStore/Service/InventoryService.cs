using WebStore.DTO;
using WebStore.Entity;
using WebStore.Repository.Interface;
using WebStore.Service.IService;

namespace WebStore.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<List<InventoryDTO>> GetAllAsync()
        {
            var inventories = await _inventoryRepository.GetAllAsync();
            return inventories.Select(i => new InventoryDTO
            {
                Id = i.Id,
                VariantId = i.Variant_Id,
                Quantity = i.Quantity
            }).ToList();
        }

        public async Task<InventoryDTO> GetByIdAsync(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id);
            if (inventory == null) return null;

            return new InventoryDTO
            {
                Id = inventory.Id,
                VariantId = inventory.Variant_Id,
                Quantity = inventory.Quantity
            };
        }

        public async Task AddAsync(InventoryDTO inventoryDto)
        {
            var inventory = new Inventory
            {
                Variant_Id = inventoryDto.VariantId,
                Quantity = inventoryDto.Quantity
            };

            await _inventoryRepository.AddAsync(inventory);
            await _inventoryRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(InventoryDTO inventoryDto)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(inventoryDto.Id);
            if (inventory != null)
            {
                inventory.Variant_Id = inventoryDto.VariantId;
                inventory.Quantity = inventoryDto.Quantity;

                await _inventoryRepository.UpdateAsync(inventory);
                await _inventoryRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _inventoryRepository.DeleteByIdAsync(id);
            await _inventoryRepository.SaveChangesAsync();
        }
    }

}
