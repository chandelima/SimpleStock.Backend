namespace SimpleStock.Domain.DTOs.Product;
public class ProductRequestDto
{
    public string Name { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}
