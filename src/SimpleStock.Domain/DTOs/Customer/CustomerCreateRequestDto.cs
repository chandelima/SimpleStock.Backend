using SimpleStock.Domain.DTOs.Address;

namespace SimpleStock.Domain.DTOs.Customer;
public class CustomerCreateRequestDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public ICollection<AddressRequestDto> Addresses { get; set; } = null!;
}
