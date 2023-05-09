using Microsoft.Extensions.Logging;
using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Core.Interfaces.Repositories.Main;
using UDCTestTask.Data.Repositories.Main;
using UDCTestTask.Infrastructure.DatabaseContext;

namespace UDCTestTask.Data.Repositories.Based;

public class UnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly TaskApplicationDbContext _applicationDbContext;

    public IEmployeeRepository EmployeeRepository { get; }

    public UnitOfWork(TaskApplicationDbContext dbContext, ILoggerFactory loggerFactory)
    {
        ILogger logger = loggerFactory.CreateLogger("logs");
        _applicationDbContext = dbContext;

        EmployeeRepository = new EmployeeRepository(_applicationDbContext, logger);
    }
    
    public void SaveChanges()
    {
        _applicationDbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _applicationDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _applicationDbContext.Dispose();
            }
        }

        _disposed = true;
    }
}
