using WebStore.Context;
using WebStore.Entity;
using WebStore.Repository.Interface;

namespace WebStore.Repository
{
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _context;

        public ColorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Color> AddAsync(Color color)
        {
            _context.Color.Add(color);
            await _context.SaveChangesAsync();
            return color;
        }

        public async Task<Color?> GetByIdAsync(int id)
        {
            return await _context.Color.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(Color color)
        {
            _context.Color.Remove(color);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
