using SimpleStock.Domain.Enums;

namespace SimpleStock.Domain.Models;
public class OrderModel : EntityModel
{
    public DateTimeOffset EmissionDate { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerModel Customer { get; set; } = null!;
    public EOrderStatus OrderStatus { get; set; }
    public ICollection<OrderItemModel> OrderItems { get; set; } = null!;
}
