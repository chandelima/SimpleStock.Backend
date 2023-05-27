using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.Models;
using SimpleStock.Infrastructure.DataContexts;

namespace SimpleStock.Data.Repositories;

public class OrderItemRepository : BaseRepository<OrderItemModel>, IOrderItemRepository
{
    public OrderItemRepository(SimpleStockDataContext context) : base(context) { }
}
