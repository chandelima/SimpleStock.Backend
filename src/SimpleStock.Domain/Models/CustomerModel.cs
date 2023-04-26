namespace SimpleStock.Domain.Models;
public class CustomerModel : EntityModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public ICollection<AddressModel> Addresses { get; set; } = null!;
    public ICollection<OrderModel> Orders { get; set; } = null!;
}
