using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Repository.Interface;

namespace WebStore.Repository
{
    public class SizeRepository : ISizeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SizeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Size>> GetAllAsync()
        {
            return await _dbContext.Size.ToListAsync();
        }

        public async Task<Size> GetByIdAsync(int id)
        {
            return await _dbContext.Size.FindAsync(id);
        }

        public async Task AddAsync(Size size)
        {
            await _dbContext.Size.AddAsync(size);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var size = await _dbContext.Size.FindAsync(id);
            if (size != null)
            {
                _dbContext.Size.Remove(size);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
