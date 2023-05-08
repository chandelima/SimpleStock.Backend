using AutoMapper;
using SimpleStock.Domain.DTOs.Product;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Profiles;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductRequestDto, ProductModel>();
        CreateMap<ProductModel, ProductResponseDto>();
    }
}
