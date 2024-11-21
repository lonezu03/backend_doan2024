using WebStore.DTO;
using WebStore.Entity;
using WebStore.Repository.Interface;
using WebStore.Service.IService;

namespace WebStore.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<List<ImageDto>> GetAllAsync()
        {
            var images = await _imageRepository.GetAllAsync();
            return images.Select(i => new ImageDto
            {
                Id = i.Id,
                Url = i.Url,
                Variant_Id = i.Variant_Id
            }).ToList();
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            return await _imageRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(ImageDto imageDto)
        {
            var image = new Image
            {
                Url = imageDto.Url,
                Variant_Id = (int)imageDto.Variant_Id
            };
            await _imageRepository.AddAsync(image);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _imageRepository.DeleteByIdAsync(id);
        }
    }

}
