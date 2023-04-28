using SimpleStock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStock.Data.Interfaces;
public interface IProductRepository : IBaseRepository<ProductModel>
{
    Task<ICollection<ProductModel>> GetByName(string name);
}
