using AutoMapper;
using SimpleStock.Domain.DTOs.Order;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Profiles;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderRequestDto, OrderModel>();
        CreateMap<OrderModel, OrderResponseDto>();
    }
}
