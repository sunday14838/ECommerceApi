using AutoMapper;
using ECommerceApi.DTOs;
using ECommerceApi.Models;

namespace ECommerceApi.Mappings
{
#pragma warning disable CS1591
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductDto, Product>().ReverseMap();

            CreateMap<Product, ProductResponseDto>().ReverseMap();

            CreateMap<UpdateProductDto, Product>().ReverseMap();

            CreateMap<CartItem, AddToCartDto>().ReverseMap();

            CreateMap<RegisterDto, User>().ReverseMap();


            //CreateMap<Order, OrderResponseDto>();
        }
    }
}
