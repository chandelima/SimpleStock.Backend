namespace SimpleStock.Domain.Model;
public class SaleModel : BaseEntityModel
{
    public DateTimeOffset EmissionDate { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerModel? Customer { get; set; }
    public IEnumerable<SaleItemModel> SaleItems { get; set; } = null!;
}
