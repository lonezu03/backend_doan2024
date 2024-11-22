using Microsoft.EntityFrameworkCore;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Repository.Interface;

namespace WebStore.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order_Item>> GetAllAsync()
        {
            return await _context.Order_Item
                .Include(oi => oi.Order)
                .Include(oi => oi.Inventory)
                .ToListAsync();
        }

        public async Task<Order_Item> GetByIdAsync(int id)
        {
            return await _context.Order_Item
                .Include(oi => oi.Order)
                .Include(oi => oi.Inventory)
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task AddAsync(Order_Item orderItem)
        {
            await _context.Order_Item.AddAsync(orderItem);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var orderItem = await GetByIdAsync(id);
            if (orderItem != null)
            {
                _context.Order_Item.Remove(orderItem);
            }
        }
        public async Task<Order_Item> UpdateAsync(Order_Item orderItem)
        {
            var entity = _context.Set<Order_Item>().Update(orderItem);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
