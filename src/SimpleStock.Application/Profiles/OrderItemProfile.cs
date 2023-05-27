using AutoMapper;
using SimpleStock.Domain.DTOs.OrderItem;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Profiles;
public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItemRequestDto, OrderItemModel>();
        CreateMap<OrderItemModel, OrderItemResponseDto>();
    }
}
