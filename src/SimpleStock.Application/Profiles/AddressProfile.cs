using AutoMapper;
using SimpleStock.Domain.DTOs.Address;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Profiles;
public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressCreateRequestDto, AddressModel>();
        CreateMap<AddressUpdateRequestDto, AddressModel>();
        CreateMap<AddressModel, AddressResponseDto>();
    }
}
