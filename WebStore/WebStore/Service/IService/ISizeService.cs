using WebStore.DTO;

namespace WebStore.Service.IService
{
    public interface ISizeService
    {
        Task<List<SizeDto>> GetAllAsync();
        Task<SizeDto> GetByIdAsync(int id);
        Task AddAsync(SizeDto sizeDto);
        Task DeleteByIdAsync(int id);
    }

}
