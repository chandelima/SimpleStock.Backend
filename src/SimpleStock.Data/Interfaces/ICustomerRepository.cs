using SimpleStock.Domain.Models;

namespace SimpleStock.Data.Interfaces;
public interface ICustomerRepository : IBaseRepository<CustomerModel>
{
    Task<ICollection<CustomerModel>> GetByName(string name);
}
