using Microsoft.EntityFrameworkCore;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.Models;
using SimpleStock.Infrastructure.DataContexts;

namespace SimpleStock.Data.Repositories;

public class OrderRepository : BaseRepository<OrderModel>, IOrderRepository
{
    private readonly SimpleStockDataContext _context;

    public OrderRepository(
        SimpleStockDataContext context) : base(context)
    {
        _context = context;    
    }

    public override async Task<OrderModel?> GetById(Guid id)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(e => !e.IsDeleted)
            .Include(e => e.OrderItems)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}
