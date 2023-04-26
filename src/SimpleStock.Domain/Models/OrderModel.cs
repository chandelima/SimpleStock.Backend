namespace SimpleStock.Domain.Models;
public class OrderModel : EntityModel
{
    public DateTimeOffset EmissionDate { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerModel Customer { get; set; } = null!;
    public ICollection<OrderItemModel> SaleItems { get; set; } = null!;
}
