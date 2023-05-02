using SimpleStock.Domain.Models;

namespace SimpleStock.Domain.DTOs.Address;
public class AddressRequestDto
{
    public string StreetName { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string? Complement { get; set; }
    public string Neighborhood { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public CustomerModel Customer { get; set; } = null!;
}
