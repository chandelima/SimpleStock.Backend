using SimpleStock.Domain.DTOs.OrderItem;

namespace SimpleStock.Domain.DTOs.Order;
public class OrderRequestDto
{
    public DateTimeOffset EmissionDate { get; set; }
    public Guid CustomerId { get; set; }
    public ICollection<OrderItemRequestDto> OrderItems { get; set; } = null!;
}