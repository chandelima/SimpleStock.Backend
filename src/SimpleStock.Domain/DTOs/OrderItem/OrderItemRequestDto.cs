using SimpleStock.Domain.Models;

namespace SimpleStock.Domain.DTOs.OrderItem;
public class OrderItemRequestDto
{
    public Guid ProductId { get; set; }
    public ProductModel Product { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid SaleId { get; set; }
    public OrderModel Sale { get; set; } = null!;
}
