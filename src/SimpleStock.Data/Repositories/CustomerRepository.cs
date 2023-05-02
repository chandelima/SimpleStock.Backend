using Microsoft.EntityFrameworkCore;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.Models;
using SimpleStock.Infrastructure.DataContexts;

namespace SimpleStock.Data.Repositories;

public class CustomerRepository : BaseRepository<CustomerModel>, ICustomerRepository
{
    private readonly SimpleStockDataContext _context;

    public CustomerRepository(SimpleStockDataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<ProductModel>> GetByName(string name)
    {
        return await _context.Products
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .OrderBy(e => e.Name)
            .ToListAsync();
    }
}
