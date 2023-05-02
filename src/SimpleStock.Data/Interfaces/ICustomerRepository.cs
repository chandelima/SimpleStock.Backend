using SimpleStock.Domain.Models;

namespace SimpleStock.Data.Interfaces;
public interface ICustomerRepository : IBaseRepository<CustomerModel>
{
    Task<ICollection<ProductModel>> GetByName(string name);
}
