using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Repository.Interface;

namespace WebStore.Repository
{
    public class DescriptionRepository : IDescriptionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DescriptionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Description>> GetAllAsync()
        {
            return await _dbContext.Description.ToListAsync();
        }

        public async Task<Description> GetByIdAsync(int id)
        {
            return await _dbContext.Description.FindAsync(id);
        }

        public async Task AddAsync(Description description)
        {
            await _dbContext.Description.AddAsync(description);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var description = await _dbContext.Description.FindAsync(id);
            if (description != null)
            {
                _dbContext.Description.Remove(description);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
