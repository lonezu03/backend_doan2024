using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Entity;

namespace WebStore.Reposiroty.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteByIdAsync(int id);
        Task<Product> GetProductByNameAsync(string name);

        Task<Product> GetByIdAsyncW(int id);
        Task SaveChangesAsync();
        Task<List<Product>> GetAllWithVariantsAsync();

    }

}
