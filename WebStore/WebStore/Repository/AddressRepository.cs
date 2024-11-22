using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Repository.Interface;

namespace WebStore.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await _context.Address
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await _context.Address
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Address address)
        {
            await _context.Address.AddAsync(address);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var address = await GetByIdAsync(id);
            if (address != null)
            {
                _context.Address.Remove(address);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
