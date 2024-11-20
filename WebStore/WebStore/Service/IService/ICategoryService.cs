using WebStore.DTO;

namespace WebStore.Service.IService
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task AddAsync(CategoryDto categoryDto);
        Task DeleteByIdAsync(int id);
    }

}
