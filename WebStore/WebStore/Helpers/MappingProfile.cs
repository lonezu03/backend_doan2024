using AutoMapper;
using WebStore.DTO;
using WebStore.Entity;

namespace WebStore.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            // User
            CreateMap<User, UserDto>().ReverseMap();

            // Product
            CreateMap<Product, ProductDto>().ReverseMap();

            // Order
           // CreateMap<Order, OrderDto>().ReverseMap();

            // OrderItem
           // CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }

    
}
