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

        public async Task<List<Variant>> GetAllAsync()
        {
            return await _variantRepository.GetAllAsync();
        }

        public async Task<Variant> GetByIdAsync(int id)
        {
            return await _variantRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Variant variant)
        {
            await _variantRepository.AddAsync(variant);
            await _variantRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _variantRepository.DeleteByIdAsync(id);
            await _variantRepository.SaveChangesAsync();
        }
    }

}
