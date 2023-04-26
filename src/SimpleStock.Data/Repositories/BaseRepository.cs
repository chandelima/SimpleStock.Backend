using Microsoft.EntityFrameworkCore;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.Models;
using SimpleStock.Infrastructure.DataContexts;

namespace SimpleStock.Data.Repositories;
public class BaseRepository : IBaseRepository
{
    private readonly SimpleStockDataContext _context;

    public BaseRepository(SimpleStockDataContext context)
    {
        _context = context;
    }

    public async Task<T?> GetById<T>(Guid id) where T : EntityModel
    {
        return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> Add<T>(T entity) where T : EntityModel
    {
        _context.Set<T>().Add(entity);
        return await SaveChangesAsync();
    }

    public async Task<bool> Update<T>(T entity) where T : EntityModel
    {
        _context.Set<T>().Update(entity);
        return await SaveChangesAsync();
    }

    public async Task<bool> Delete<T>(T entity) where T : EntityModel
    {
        _context.Set<T>().Remove(entity);
        return await SaveChangesAsync();
    }

    public async Task<bool> DeleteRange<T>(T[] entities) where T : EntityModel
    {
        _context.Set<T>().RemoveRange(entities);
        return await SaveChangesAsync();
    }

    private async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}
