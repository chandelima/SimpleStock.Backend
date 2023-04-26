namespace SimpleStock.Domain.Models;
public class ProductModel : EntityModel
{
    public string Name { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}
