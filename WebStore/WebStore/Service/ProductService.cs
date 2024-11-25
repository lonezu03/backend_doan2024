using AutoMapper;
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
        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }
        public async Task CreateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(product);
        }
        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(product);
        }
        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteByIdAsync(id);
        }
    }

}
