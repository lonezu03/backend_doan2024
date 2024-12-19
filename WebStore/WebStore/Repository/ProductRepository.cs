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
        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _context.Product
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Product
                .Include(p => p.Material)
                .Include(p => p.Gender)
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Product> GetByIdAsyncW(int id)
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
                
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Product product)
        {
            await _context.Product.AddAsync(product);

        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var existingEntity = await _context.Product.FindAsync(product.Id);
            if (existingEntity != null)
            {
                // Chỉ cập nhật các trường nếu giá trị mới khác giá trị hiện tại
                if (!string.IsNullOrWhiteSpace(product.Name) && product.Name != existingEntity.Name)
                    _context.Entry(existingEntity).Property(e => e.Name).CurrentValue = product.Name;

                if (!string.IsNullOrWhiteSpace(product.Description) && product.Description != existingEntity.Description && product.Description != null)
                    _context.Entry(existingEntity).Property(e => e.Description).CurrentValue = product.Description;

                if (!string.IsNullOrWhiteSpace(product.Status) && product.Status != existingEntity.Status)
                    _context.Entry(existingEntity).Property(e => e.Status).CurrentValue = product.Status;
                if (product.price > 0 && product.price != existingEntity.price)
                    _context.Entry(existingEntity).Property(e => e.price).CurrentValue = product.price;
                if (product.Gender_Id > 0 && product.Gender_Id != existingEntity.Gender_Id)
                    _context.Entry(existingEntity).Property(e => e.Gender_Id).CurrentValue = product.Gender_Id;

                if (product.Material_Id > 0 && product.Material_Id != existingEntity.Material_Id && product.Material_Id!=null )
                    _context.Entry(existingEntity).Property(e => e.Material_Id).CurrentValue = product.Material_Id;


                // Đánh dấu thực thể là đã chỉnh sửa
                _context.Entry(existingEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return existingEntity;
            }

            throw new KeyNotFoundException("Product not found");
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
                //.Include(p=>p.Variants)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Color)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Size)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Description)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Category)
                
                .ToListAsync();
        }

    }
}