using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Repository.Interface;

namespace WebStore.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ImageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Image>> GetAllAsync()
        {
            return await _dbContext.Image.Include(i => i.Variant).ToListAsync();
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            return await _dbContext.Image.FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task AddAsync(Image image)
        {
            await _dbContext.Image.AddAsync(image);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var image = await _dbContext.Image.FindAsync(id);
            if (image != null)
            {
                _dbContext.Image.Remove(image);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
