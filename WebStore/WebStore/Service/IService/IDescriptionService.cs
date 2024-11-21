using WebStore.DTO;

namespace WebStore.Service.IService
{
    public interface IDescriptionService
    {
        Task<List<DescriptionDto>> GetAllAsync();
        Task<DescriptionDto> GetByIdAsync(int id);
        Task AddAsync(DescriptionDto descriptionDto);
        Task DeleteByIdAsync(int id);
    }

}
