using SimpleStock.Domain.DTOs.OrderItem;

namespace SimpleStock.Domain.DTOs.Order;
public class OrderCreateDto
{
    public DateTimeOffset EmissionDate { get; set; }
    public Guid CustomerId { get; set; }
    public ICollection<OrderItemCreateDto> OrderItems { get; set; } = null!;
}