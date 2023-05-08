using SimpleStock.Domain.Models;

namespace SimpleStock.Domain.DTOs.Address;
public class AddressResponseDto
{
    public Guid Id { get; set; }
    public string StreetName { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string? Complement { get; set; }
    public string Neighborhood { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}
