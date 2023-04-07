namespace SimpleStock.Domain.Model;
public class SaleItemModel : BaseEntityModel
{
    public Guid ProductId { get; set; }
    public ProductModel? Product { get; set; }
    public decimal Amount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid SaleId { get; set; }
    public SaleModel? Sale { get; set; }
}
