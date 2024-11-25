using AutoMapper;
using WebStore.DTO;
using WebStore.Entity;

namespace WebStore.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            // User
            CreateMap<Users, UserDto>().ReverseMap();

            // Product
            CreateMap<Product, ProductDto>().ReverseMap();

            // Order
           CreateMap<Orders, OrderDto>().ReverseMap();

            // OrderItem
            // CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            //Size
            CreateMap<Size, SizeDto>().ReverseMap();
            //category
            CreateMap<Category, CategoryDto>().ReverseMap();
            //variant
            CreateMap<Variant, VariantDto>().ReverseMap();
            //img
            CreateMap<Image,ImageDto>().ReverseMap();
            //Description
            CreateMap<Description, DescriptionDto>().ReverseMap();
            //Material
            CreateMap<Material, MaterialDto>().ReverseMap();
            //Gender
            CreateMap<Gender, GenderDto>().ReverseMap();
            //Product
            CreateMap<Product, ProductDto>().ReverseMap();

        }
    }

    
}
