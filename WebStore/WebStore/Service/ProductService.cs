using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.DTO;
using WebStore.Entity;
using WebStore.Reposiroty.Interface;
using WebStore.Service.IService;


namespace WebStore.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository , IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto, string imageUrl)
        {
            // Chuyển đổi DTO sang entity
            
            var product = new Product
            {
                Name = productDto.Name,
                Material_Id = productDto.Material_Id,
                Description = productDto.Description,
                Status = productDto.Status,
                price = productDto.price,
                Gender_Id = productDto.Gender_Id,
                Image = imageUrl // Lưu URL ảnh vào sản phẩm
            };

            // Thêm sản phẩm vào cơ sở dữ liệu
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return product;
        }
        public async Task<Product> GetProductByNameAsync(string name)
        {
            return await _productRepository.GetProductByNameAsync(name);
        }

        public async Task UpdateProductAsync(int id, ProductDto productDto, string imageUrl)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productDto.Id);
                if (product != null)
                {
                    if (!string.IsNullOrWhiteSpace(productDto.Name))
                        product.Name = productDto.Name;
                    if (productDto.Material_Id != 0 && productDto.Material_Id != null)
                        product.Material_Id = productDto.Material_Id;
                    if (!string.IsNullOrWhiteSpace(productDto.Description))
                        product.Description = productDto.Description;
                    if (!string.IsNullOrWhiteSpace(productDto.Status))
                        product.Status = productDto.Status;
                    if (productDto.price != 0)
                        product.price = productDto.price;
                    if (productDto.price != 0 && productDto.price != null)
                        product.Gender_Id = productDto.Gender_Id;
                    if (imageUrl!=null)
                        product.Image = imageUrl; // Lưu URL ảnh vào sản phẩm
                };


                await _productRepository.UpdateAsync(product);
                await _productRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log lỗi ra console hoặc file
                Console.WriteLine($"Error during SaveChanges: {ex.Message}");
                throw; // Ném lỗi ra ngoài để Controller xử lý
            }
            
        }
      


        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteByIdAsync(id);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllProductsWithVariantsAsync()
        {
            var products = await _productRepository.GetAllWithVariantsAsync();

            return products.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                price=p.price,
                image=p.Image,
                description = p.Description,
                Material =p.Material_Id,
                gender=p.Gender_Id,

                colors = p.Variants
                    .Select(v => new { id = v.Color.Id, name = v.Color.Name })
                    .Distinct()
                    .ToList(),
                sizes = p.Variants
                    .Select(v => new { id = v.Size.Id, name = v.Size.Name })
                    .Distinct()
                    .ToList(),
                //images = p.Variants
                //    .SelectMany(v => v.Images)
                //    .Select(i => new { id = i.Id, url = i.Url })
                //    .Distinct()
                //    .ToList(),
                //description = p.Variants
                //    .Select(v => v.Description)
                //    .Distinct()
                //    .Select(d => new { id = d.Id, title = d.Title, content = d.Content })

                //    .FirstOrDefault(),
                category=p.Variants
                     .Select(v => v.Category)
                    .Distinct()
                    .Select(d => new { id = d.Id, name = d.Name})
                    .FirstOrDefault(),
                variants = p.Variants.Select(v => new
                {
                    id = v.Id,
                    color_id = v.Color_Id,
                    size_id = v.Size_Id,
                    description_id = v.Description_Id,
                    category_id = v.Category_Id,
                    //image= v.Image
                }).ToList()
            });
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Material_Id = product.Material_Id,
                Description = product.Description,
                Status = product.Status,
                price = product.price,
                Gender_Id = product.Gender_Id,
                Image = product.Image
            };
        }
        public async Task<object> GetProductByIdAsync(int id)
        {
            // Lấy sản phẩm theo ID
            var product = await _productRepository.GetByIdAsyncW(id);

            // Kiểm tra nếu sản phẩm không tồn tại
            if (product == null)
            {
                return null;
            }

            return new
            {
                id = product.Id,
                name = product.Name,
                Image = product.Image,
                Material = product.Material_Id,
                description=product.Description,
                Gender = product.Gender_Id,
                price = product.price,
                colors = product.Variants
                    .Select(v => new { id = v.Color.Id, name = v.Color.Name })
                    .Distinct()
                    .ToList(),
                sizes = product.Variants
                    .Select(v => new { id = v.Size.Id, name = v.Size.Name })
                    .Distinct()
                    .ToList(),
                
                //description = product.Variants
                //    .Select(v => v.Description)
                //    .Distinct()
                //    .Select(d => new { id = d.Id, title = d.Title, content = d.Content })
                //    .FirstOrDefault(),
                variants = product.Variants.Select(v => new
                {
                    id = v.Id,
                    color_id = v.Color_Id,
                    size_id = v.Size_Id,
                    description_id = v.Description_Id,
                    category_id = v.Category_Id
                    
                }).ToList()
            };
        }


    }

}
