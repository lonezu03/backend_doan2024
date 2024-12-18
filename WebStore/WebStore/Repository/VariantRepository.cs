using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Repository.Interface;

namespace WebStore.Repository
{
    public class VariantRepository : IVariantRepository
    {
        private readonly ApplicationDbContext _context;

        public VariantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Variant>> GetAllAsync()
        {
            return await _context.Variant
                .Include(v => v.Product)
                .Include(v => v.Color)
                .Include(v => v.Size)
                .Include(v => v.Description)
                .Include(v => v.Category)
                .ToListAsync();
        }

        public async Task<Variant> GetByIdAsync(int id)
        {
            return await _context.Variant
                .Include(v => v.Product)
                .Include(v => v.Color)
                .Include(v => v.Size)
                .Include(v => v.Description)
                .Include(v => v.Category)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddAsync(Variant variant)
        {
            await _context.Variant.AddAsync(variant);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var variant = await GetByIdAsync(id);
            if (variant != null)
            {
                _context.Variant.Remove(variant);
            }
        }
        public async Task<Variant> UpdateAsync(Variant variant)
        {
            var existingEntity = await _context.Set<Variant>().FindAsync(variant.Id);
            if (existingEntity != null)
            {
                // Chỉ cập nhật các trường được thay đổi
                if (variant.Product_Id > 0 && variant.Product_Id != existingEntity.Product_Id)
                    _context.Entry(existingEntity).Property(e => e.Product_Id).CurrentValue = variant.Product_Id;

                if (variant.Color_Id > 0 && variant.Color_Id != existingEntity.Color_Id)
                    _context.Entry(existingEntity).Property(e => e.Color_Id).CurrentValue = variant.Color_Id;

                if (variant.Size_Id > 0 && variant.Size_Id != existingEntity.Size_Id)
                    _context.Entry(existingEntity).Property(e => e.Size_Id).CurrentValue = variant.Size_Id;

                if (variant.Description_Id > 0 && variant.Description_Id != existingEntity.Description_Id)
                    _context.Entry(existingEntity).Property(e => e.Description_Id).CurrentValue = variant.Description_Id;

                if (variant.Category_Id > 0 && variant.Category_Id != existingEntity.Category_Id)
                    _context.Entry(existingEntity).Property(e => e.Category_Id).CurrentValue = variant.Category_Id;

                _context.Entry(existingEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return existingEntity;
            }
            throw new KeyNotFoundException("variant not found");

        }
        public async Task<IEnumerable<Variant>> GetByProductIdAsync(int productId)
        {
            return await _context.Variant.Where(v => v.Product_Id == productId).ToListAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Variant> variants)
        {
            _context.Variant.RemoveRange(variants);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
