namespace SimpleStock.Domain.DTOs.Product;
public class ProductInputModel
{
    public string Name { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}
