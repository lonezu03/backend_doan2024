using WebStore.Entity;

namespace WebStore.Service.IService
{
    public interface IColorService
    {
        Task<Color> AddColorAsync(string name);
        Task<bool> DeleteColorAsync(int id);
    }
}
