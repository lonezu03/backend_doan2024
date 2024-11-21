using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface IImageRepository
    {
        Task<List<Image>> GetAllAsync();
        Task<Image> GetByIdAsync(int id);
        Task AddAsync(Image image);
        Task DeleteByIdAsync(int id);
    }

}
