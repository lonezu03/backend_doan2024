using WebStore.DTO;
using WebStore.Entity;
using WebStore.Repository.Interface;
using WebStore.Service.IService;

namespace WebStore.Service
{
    public class VariantService : IVariantService
    {
        private readonly IVariantRepository _variantRepository;

        public VariantService(IVariantRepository variantRepository)
        {
            _variantRepository = variantRepository;
        }

        // Lấy tất cả biến thể dưới dạng VariantDto
        public async Task<List<VariantDto>> GetAllAsync()
        {
            var variants = await _variantRepository.GetAllAsync();
            return variants.Select(v => new VariantDto
            {
                Id = v.Id,
                Product_Id = v.Product_Id,
                Color_Id = v.Color_Id,
                Size_Id = v.Size_Id,
                Description_Id = v.Description_Id,
                Category_Id = v.Category_Id
            }).ToList();
        }

        // Lấy biến thể theo ID dưới dạng VariantDto
        public async Task<VariantDto> GetByIdAsync(int id)
        {
            var variant = await _variantRepository.GetByIdAsync(id);
            if (variant == null) return null;

            return new VariantDto
            {
                Id = variant.Id,
                Product_Id = variant.Product_Id,
                Color_Id = variant.Color_Id,
                Size_Id = variant.Size_Id,
                Description_Id = variant.Description_Id,
                Category_Id = variant.Category_Id
            };
        }

        // Thêm biến thể từ VariantDto
        public async Task AddAsync(VariantDto variantDto)
        {
            var variant = new Variant
            {
                Product_Id = variantDto.Product_Id,
                Color_Id = variantDto.Color_Id,
                Size_Id = variantDto.Size_Id,
                Description_Id = variantDto.Description_Id,
                Category_Id = variantDto.Category_Id
            };

            await _variantRepository.AddAsync(variant);
            await _variantRepository.SaveChangesAsync();
        }

        // Xóa biến thể theo ID
        public async Task DeleteByIdAsync(int id)
        {
            await _variantRepository.DeleteByIdAsync(id);
            await _variantRepository.SaveChangesAsync();
        }
    }
}
