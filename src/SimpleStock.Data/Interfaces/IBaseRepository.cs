using SimpleStock.Domain.Models;

namespace SimpleStock.Data.Interfaces;
public interface IBaseRepository<T>
{
    Task<ICollection<T>> GetAll();
    Task<T?> GetById(Guid id);
    Task<bool> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
    Task<bool> DeleteRange(T[] entities);
}
