using Microsoft.Extensions.Logging;
using Moq;
using UDCTestTask.Infrastructure.DatabaseContext;

namespace UDCTestTask.Shared;

public class TestBase<T> : IDisposable
{
    protected readonly TaskApplicationDbContext ApplicationDbContext;
    protected readonly ILogger<T> Logger;
    
    public TestBase()
    {
        ApplicationDbContext = TaskApplicationDbContextFactory.CreateDatabaseWithData();
        Logger = Mock.Of<ILogger<T>>();
    }

    public void Dispose()
    {
        TaskApplicationDbContextFactory.Destroy(ApplicationDbContext);
    }
}
