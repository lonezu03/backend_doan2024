using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface IVariantRepository
    {
        Task<List<Variant>> GetAllAsync();
        Task<Variant> GetByIdAsync(int id);
        Task AddAsync(Variant variant);
        Task DeleteByIdAsync(int id);
        Task SaveChangesAsync();
    }

}
