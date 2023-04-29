using SimpleStock.Domain.Models;

namespace SimpleStock.Data.Interfaces;
public interface IProductRepository : IBaseRepository<ProductModel>
{
    Task<ICollection<ProductModel>> GetByName(string name);
}
