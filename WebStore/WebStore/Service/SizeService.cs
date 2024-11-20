using WebStore.DTO;
using WebStore.Entity;
using WebStore.Repository.Interface;
using WebStore.Service.IService;

namespace WebStore.Service
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public async Task<List<SizeDto>> GetAllAsync()
        {
            var sizes = await _sizeRepository.GetAllAsync();
            return sizes.Select(s => new SizeDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public async Task<SizeDto> GetByIdAsync(int id)
        {
            var size = await _sizeRepository.GetByIdAsync(id);
            if (size == null) return null;

            return new SizeDto
            {
                Id = size.Id,
                Name = size.Name
            };
        }

        public async Task AddAsync(SizeDto sizeDto)
        {
            var size = new Size
            {
                Name = sizeDto.Name
            };
            await _sizeRepository.AddAsync(size);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _sizeRepository.DeleteByIdAsync(id);
        }
    }

}
