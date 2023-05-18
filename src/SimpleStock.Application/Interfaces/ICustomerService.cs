using SimpleStock.Domain.DTOs.Customer;
using SimpleStock.Domain.Models;

namespace SimpleStock.Application.Interfaces;
public interface ICustomerService
{
    Task<ICollection<CustomerResponseDto>> GetAll();
    Task<CustomerResponseDto?> GetById(Guid id);
    Task<ICollection<CustomerModel>> GetByName(string name);
    Task<CustomerResponseDto?> AddCustomer(CustomerCreateRequestDto request);
    Task<CustomerResponseDto?> UpdateCustomer(Guid id, CustomerUpdateRequestDto request);
    Task<bool> DeleteCustomer(Guid id);
}
