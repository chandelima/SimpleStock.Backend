namespace SimpleStock.Domain.DTOs.Product;
public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
