using SimpleStock.Domain.DTOs.Order;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Interfaces;
public interface IOrderService
{
    Task<ICollection<OrderResponseDto>> GetAll();
    Task<OrderResponseDto?> GetById(Guid id);
    Task<OrderResponseDto?> AddOrder(OrderRequestDto request);
    Task<OrderResponseDto?> UpdateOrder(Guid id, OrderRequestDto request);
    Task<bool> DeleteOrder(Guid id);
}
