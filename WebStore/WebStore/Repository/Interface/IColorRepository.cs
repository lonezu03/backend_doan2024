using WebStore.Entity;

namespace WebStore.Repository.Interface
{
    public interface IColorRepository
    {
        Task<Color> AddAsync(Color color);
        Task<Color?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(Color color);
    }
}
