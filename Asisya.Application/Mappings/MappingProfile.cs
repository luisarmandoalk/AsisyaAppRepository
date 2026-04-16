using AutoMapper;
using Asisya.Domain.Entities;
using Asisya.Application.DTOs;

namespace Asisya.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product → ProductDto
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Category.Picture));

            // CreateProductDto → Product
            CreateMap<CreateProductDto, Product>();

            // Category → CategoryDto
            CreateMap<Category, CategoryDto>();

            // CategoryDto → Category (opcional)
            CreateMap<CategoryDto, Category>();
        }
    }
}