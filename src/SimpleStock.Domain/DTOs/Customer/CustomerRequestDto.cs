using SimpleStock.Domain.DTOs.Address;
using SimpleStock.Domain.DTOs.Order;
using SimpleStock.Domain.Models;

namespace SimpleStock.Domain.DTOs.Customer;
public class CustomerRequestDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string CPF { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public ICollection<AddressRequestDto> Addresses { get; set; } = null!;
    public ICollection<OrderRequestDto> Orders { get; set; } = null!;
}
