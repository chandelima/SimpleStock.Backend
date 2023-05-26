namespace SimpleStock.Domain.DTOs.OrderItem;
public class OrderItemUpdateDto
{
    public Guid? Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal Amount { get; set; }
}
