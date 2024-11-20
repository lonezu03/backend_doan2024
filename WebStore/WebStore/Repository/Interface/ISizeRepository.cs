using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface ISizeRepository
    {
        Task<List<Size>> GetAllAsync();
        Task<Size> GetByIdAsync(int id);
        Task AddAsync(Size size);
        Task DeleteByIdAsync(int id);
    }

}
