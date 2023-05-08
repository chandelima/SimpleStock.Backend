using SimpleStock.Domain.DTOs.Address;

namespace SimpleStock.Domain.DTOs.Customer;
public class CustomerResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public ICollection<AddressResponseDto> Addresses { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}
