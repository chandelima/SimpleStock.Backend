namespace SimpleStock.Domain.Models;
public class OrderItemModel : EntityModel
{
    public Guid ProductId { get; set; }
    public ProductModel Product { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get => GetTotalPrice(); }
    public Guid OrderId { get; set; }
    public OrderModel Order { get; set; } = null!;

    private decimal GetTotalPrice() => Amount * UnitPrice;
}
