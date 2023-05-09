using System.Linq.Expressions;

namespace UDCTestTask.Core.Interfaces.Repositories.Based;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>>? filter, Func<IQueryable<T>,
        IOrderedQueryable<T>>? orderBy, string includeProperties = "");
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}