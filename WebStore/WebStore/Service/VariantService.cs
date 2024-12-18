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
                Category_Id = v.Category_Id,
                Image = v.Image
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
        // Cập nhật Variant

        public async Task UpdateVariantAsync(VariantDto variantDto)
        {
            var variant = await _variantRepository.GetByIdAsync(variantDto.Id);
            if (variant == null)
            {
                throw new KeyNotFoundException("Variant not found");
            }

            // Chỉ cập nhật các thuộc tính có giá trị khác null hoặc khác giá trị hiện tại
            if (variantDto.Product_Id > 0 && variantDto.Product_Id != variant.Product_Id)
                variant.Product_Id = variantDto.Product_Id;

            if (variantDto.Color_Id>0 && variantDto.Color_Id != variant.Color_Id)
                variant.Color_Id = variantDto.Color_Id.Value;

            if (variantDto.Size_Id>0 && variantDto.Size_Id != variant.Size_Id)
                variant.Size_Id = variantDto.Size_Id.Value;

            if (variantDto.Description_Id>0 && variantDto.Description_Id != variant.Description_Id)
                variant.Description_Id = variantDto.Description_Id.Value;

            if (variantDto.Category_Id>0 && variantDto.Category_Id != variant.Category_Id)
                variant.Category_Id = variantDto.Category_Id.Value;

            // Gửi thực thể đã cập nhật vào repository
            await _variantRepository.UpdateAsync(variant);
            await _variantRepository.SaveChangesAsync();
        }
        public async Task DeleteByProductIdAsync(int productId)
        {
            var variants = await _variantRepository.GetByProductIdAsync(productId);
            if (variants.Any())
            {
                await _variantRepository.DeleteRangeAsync(variants);
                await _variantRepository.SaveChangesAsync();
            }
        }


    }
}
