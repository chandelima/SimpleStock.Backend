using AutoMapper;
using SimpleStock.Domain.DTOs.Customer;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Profiles;
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerCreateRequestDto, CustomerModel>();
        CreateMap<CustomerUpdateRequestDto, CustomerModel>();
        CreateMap<CustomerModel, CustomerResponseDto>();
    }
}
