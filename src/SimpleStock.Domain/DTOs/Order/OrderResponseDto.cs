using SimpleStock.Domain.DTOs.OrderItem;
using SimpleStock.Domain.Models;

namespace SimpleStock.Domain.DTOs.Order;
public class OrderResponseDto
{
    public Guid Id { get; set; }
    public DateTimeOffset EmissionDate { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerModel Customer { get; set; } = null!;
    public ICollection<OrderItemResponseDto> OrderItems { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}