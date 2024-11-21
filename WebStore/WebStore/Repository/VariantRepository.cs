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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
