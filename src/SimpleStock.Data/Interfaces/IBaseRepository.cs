using SimpleStock.Domain.Models;

namespace SimpleStock.Data.Interfaces;
public interface IBaseRepository
{
    Task<T?> GetById<T>(Guid id) where T : EntityModel;
    Task<bool> Add<T>(T entity) where T : EntityModel;
    Task<bool> Update<T>(T entity) where T : EntityModel;
    Task<bool> Delete<T>(T entity) where T : EntityModel;
    Task<bool> DeleteRange<T>(T[] entities) where T : EntityModel;
}
