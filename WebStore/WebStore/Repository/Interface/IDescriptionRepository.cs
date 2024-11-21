using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface IDescriptionRepository
    {
        Task<List<Description>> GetAllAsync();
        Task<Description> GetByIdAsync(int id);
        Task AddAsync(Description description);
        Task DeleteByIdAsync(int id);
    }

}
