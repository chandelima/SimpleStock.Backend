using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.Models;
using SimpleStock.Infrastructure.DataContexts;

namespace SimpleStock.Data.Repositories;

public class OrderRepository : BaseRepository<OrderModel>, IOrderRepository
{
    public OrderRepository(SimpleStockDataContext context) : base(context) { }
}
