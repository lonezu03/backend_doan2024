using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DTO;
using WebStore.Entity;

namespace WebStore.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<object> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(ProductDto productDto, string imageUrl);
        Task UpdateProductAsync(int id, ProductDto productDto, string imageUrl);
        Task DeleteProductAsync(int id);
        Task<ProductDto> GetByIdAsync(int id);
        Task<Product> GetProductByNameAsync(string name);


        Task<IEnumerable<object>> GetAllProductsWithVariantsAsync();
    }

}
