using WebStore.DTO;
using WebStore.Entity;

namespace WebStore.Service.IService
{
    public interface IVariantService
    {
        Task<List<VariantDto>> GetAllAsync();
        Task<VariantDto> GetByIdAsync(int id);
        Task AddAsync(VariantDto variant);
        Task DeleteByIdAsync(int id);
        Task UpdateVariantAsync(VariantDto variantDto);
        Task DeleteByProductIdAsync(int productId);
    }

}
