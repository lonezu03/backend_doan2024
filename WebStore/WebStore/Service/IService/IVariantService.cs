using WebStore.Entity;

namespace WebStore.Service.IService
{
    public interface IVariantService
    {
        Task<List<Variant>> GetAllAsync();
        Task<Variant> GetByIdAsync(int id);
        Task AddAsync(Variant variant);
        Task DeleteByIdAsync(int id);
    }

}
