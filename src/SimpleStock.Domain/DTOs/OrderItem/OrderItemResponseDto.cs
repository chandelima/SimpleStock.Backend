using SimpleStock.Domain.Models;

namespace SimpleStock.Domain.DTOs.OrderItem;
public class OrderItemResponseDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public ProductModel Product { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid OrderId { get; set; }
    public OrderModel Order { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}
