using WebStore.DTO;
using WebStore.Entity;

namespace WebStore.Service.IService
{
    public interface IImageService
    {
        Task<List<ImageDto>> GetAllAsync();
        Task<Image> GetByIdAsync(int id);
        Task AddAsync(ImageDto imageDto);
        Task DeleteByIdAsync(int id);
    }

}
