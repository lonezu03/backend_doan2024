using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task DeleteByIdAsync(int id);
    }

}
