using SimpleStock.Domain.DTOs.OrderItem;
using SimpleStock.Domain.Models;

namespace SimpleStock.Domain.DTOs.Order;
public class OrderRequestDto
{
    public DateTimeOffset EmissionDate { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerModel Customer { get; set; } = null!;
    public ICollection<OrderItemRequestDto> OrderItems { get; set; } = null!;
}