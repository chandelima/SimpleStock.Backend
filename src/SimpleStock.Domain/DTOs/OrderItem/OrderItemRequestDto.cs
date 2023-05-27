namespace SimpleStock.Domain.DTOs.OrderItem;
public class OrderItemRequestDto
{
    public Guid? Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal Amount { get; set; }
}
