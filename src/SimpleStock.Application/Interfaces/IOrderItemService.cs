using SimpleStock.Domain.DTOs.OrderItem;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Interfaces;
public interface IOrderItemService
{
    Task<ICollection<OrderItemResponseDto>> GetAll();
    Task<OrderItemResponseDto?> GetById(Guid id);
    Task<OrderItemResponseDto?> AddOrderItem(OrderItemCreateDto request);
    Task<OrderItemResponseDto?> UpdateOrderItem(Guid id, OrderItemCreateDto request);
    Task<bool> DeleteOrderItem(Guid id);
    Task<ICollection<OrderItemModel>> ProcessCreateOrderItems(ICollection<OrderItemCreateDto> items);
}
