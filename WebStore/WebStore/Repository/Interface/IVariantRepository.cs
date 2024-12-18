using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface IVariantRepository
    {
        Task<List<Variant>> GetAllAsync();
        Task<Variant> GetByIdAsync(int id);
        Task AddAsync(Variant variant);
        Task DeleteByIdAsync(int id);
        Task<Variant> UpdateAsync(Variant variant); 
        Task SaveChangesAsync();
        Task<IEnumerable<Variant>> GetByProductIdAsync(int productId);
        Task DeleteRangeAsync(IEnumerable<Variant> variants);
    }

}
