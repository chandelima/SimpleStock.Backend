using SimpleStock.Domain.DTOs.OrderItem;

namespace SimpleStock.Domain.DTOs.Order;
public class OrderUpdateDto
{
    public DateTimeOffset EmissionDate { get; set; }
    public Guid CustomerId { get; set; }
    public ICollection<OrderItemUpdateDto> OrderItems { get; set; } = null!;
}