using Microsoft.EntityFrameworkCore;
using System;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Repository.Interface;

namespace WebStore.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> GetAllAsync()
        {
            return await _context.Inventory
                .Include(i => i.Variant)
                .ToListAsync();
        }

        public async Task<Inventory> GetByIdAsync(int id)
        {
            return await _context.Inventory
                .Include(i => i.Variant)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Inventory inventory)
        {
            await _context.Inventory.AddAsync(inventory);
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            _context.Inventory.Update(inventory);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var inventory = await GetByIdAsync(id);
            if (inventory != null)
            {
                _context.Inventory.Remove(inventory);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}