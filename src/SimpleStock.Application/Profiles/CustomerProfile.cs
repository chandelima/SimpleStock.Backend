using AutoMapper;
using SimpleStock.Domain.DTOs.Customer;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Profiles;
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerRequestDto, CustomerModel>();
        CreateMap<CustomerModel, CustomerResponseDto>()
            .IncludeAllDerived();
    }
}
