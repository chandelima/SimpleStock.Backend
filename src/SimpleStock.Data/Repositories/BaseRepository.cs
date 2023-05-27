using Microsoft.EntityFrameworkCore;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.Models;
using SimpleStock.Infrastructure.DataContexts;

namespace SimpleStock.Data.Repositories;
public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityModel
{
    private readonly SimpleStockDataContext _context;

    protected BaseRepository(SimpleStockDataContext context)
    {
        _context = context;
    }

    public virtual async Task<ICollection<T>> GetAll()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .Where(e => !e.IsDeleted)
            .ToListAsync();
    }

    public virtual async Task<T?> GetById(Guid id)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .Where(e => !e.IsDeleted)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return await SaveChangesAsync();
    }

    public virtual async Task<bool> Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return await SaveChangesAsync();
    }

    public async Task<bool> Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return await SaveChangesAsync();
    }

    public async Task<bool> DeleteRange(T[] entities)
    {
        _context.Set<T>().RemoveRange(entities);
        return await SaveChangesAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
}
