using WebStore.Repository;
using WebStore.Service.IService;
using WebStore.Entity;
using WebStore.Context;


namespace WebStore.Service
{
    public class ColorService : IColorService
    {
        private readonly ApplicationDbContext _context;

        public ColorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Color> AddColorAsync(string name)
        {
            var color = new Color { Name = name };
            _context.Color.Add(color);
            await _context.SaveChangesAsync();
            return color;
        }

        public async Task<bool> DeleteColorAsync(int id)
        {
            var color = await _context.Color.FindAsync(id);
            if (color == null) return false;

            _context.Color.Remove(color);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
