using WebStore.DTO;
using WebStore.Entity;
using WebStore.Repository.Interface;
using WebStore.Service.IService;

namespace WebStore.Service
{
    public class DescriptionService : IDescriptionService
    {
        private readonly IDescriptionRepository _descriptionRepository;

        public DescriptionService(IDescriptionRepository descriptionRepository)
        {
            _descriptionRepository = descriptionRepository;
        }

        public async Task<List<DescriptionDto>> GetAllAsync()
        {
            var descriptions = await _descriptionRepository.GetAllAsync();
            return descriptions.Select(d => new DescriptionDto
            {
                Id = d.Id,
                Title = d.Title,
                Content = d.Content
            }).ToList();
        }

        public async Task<DescriptionDto> GetByIdAsync(int id)
        {
            var description = await _descriptionRepository.GetByIdAsync(id);
            if (description == null) return null;

            return new DescriptionDto
            {
                Id = description.Id,
                Title = description.Title,
                Content = description.Content
            };
        }

        public async Task AddAsync(DescriptionDto descriptionDto)
        {
            var description = new Description
            {
                Title = descriptionDto.Title,
                Content = descriptionDto.Content
            };
            await _descriptionRepository.AddAsync(description);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _descriptionRepository.DeleteByIdAsync(id);
        }
    }

}
