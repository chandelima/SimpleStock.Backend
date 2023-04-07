namespace SimpleStock.Domain.Model;
public class ProductModel : BaseEntityModel
{
    public string Name { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}
