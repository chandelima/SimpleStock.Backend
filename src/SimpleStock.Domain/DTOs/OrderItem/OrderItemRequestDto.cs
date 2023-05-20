using SimpleStock.Domain.Models;

namespace SimpleStock.Domain.DTOs.OrderItem;
public class OrderItemRequestDto
{
    public Guid ProductId { get; set; }
    public decimal Amount { get; set; }
}
