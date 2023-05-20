using AutoMapper;
using SimpleStock.Domain.DTOs.Address;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Profiles;
public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressRequestDto, AddressModel>();
        CreateMap<AddressModel, AddressResponseDto>();
    }
}
