using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Infrastructure.DatabaseContext;

namespace UDCTestTask.Data.Repositories.Based;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<T> DbSet;
    protected readonly ILogger Logger;

    public GenericRepository(TaskApplicationDbContext dbContext, ILogger logger)
    {
        DbContext = dbContext;
        Logger = logger;
        DbSet = DbContext.Set<T>();
    }
    
    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>>? filter, Func<IQueryable<T>,
        IOrderedQueryable<T>>? orderBy, string includeProperties = "")
    {
        IQueryable<T> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        IEnumerable<T> entities = await DbSet.ToListAsync();
        return entities;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        T? entity = await DbSet.FindAsync(id);

        if (entity is null)
            throw new NullReferenceException();

        return entity;
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(T entity)
    {
        if (DbContext.Entry(entity).State == EntityState.Detached)
            DbSet.Attach(entity);
            
        DbSet.Remove(entity);
    }
}
