using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Context;
using WebStore.Entity;
using WebStore.Reposiroty.Interface;

namespace WebStore.Reposiroty
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Product
                .Include(p => p.Material)
                .Include(p => p.Gender)
                .Include(p => p.Variants)
                .ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Product
                .Include(p => p.Material)
                .Include(p => p.Gender)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Product product)
        {
            await _context.Product.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Product.Update(product);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetAllWithVariantsAsync()
        {
            return await _context.Product
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Color)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Size)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Description)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Category)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Images)
                .ToListAsync();
        }

    }
}