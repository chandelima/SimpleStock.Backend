using SimpleStock.Domain.DTOs.OrderItem;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Interfaces;
public interface IOrderItemService
{
    Task<ICollection<OrderItemResponseDto>> GetAll();
    Task<OrderItemResponseDto?> GetById(Guid id);
    Task<OrderItemResponseDto?> AddOrderItem(OrderItemRequestDto request);
    Task<OrderItemResponseDto?> UpdateOrderItem(Guid id, OrderItemRequestDto request);
    Task<bool> DeleteOrderItem(Guid id);
    Task DecreaseOrdemItemsStock(ICollection<OrderItemModel> items);
    Task<ICollection<OrderItemModel>> ProcessOrderItems(ICollection<OrderItemRequestDto> items);
}
