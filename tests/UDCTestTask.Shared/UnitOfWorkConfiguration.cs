using Microsoft.EntityFrameworkCore;
using Moq;
using UDCTestTask.Infrastructure.DatabaseContext;

namespace UDCTestTask.Shared;

public static class UnitOfWorkConfiguration
{
    public static Mock<TaskApplicationDbContext> CreateMockApplicationDbContext()
    {
        DbContextOptions<TaskApplicationDbContext> applicationContextOptions =
            CreateDbContextOptions<TaskApplicationDbContext>();
        
        Mock<TaskApplicationDbContext> mockApplicationDbContext = 
            new Mock<TaskApplicationDbContext>(applicationContextOptions);
        
        return mockApplicationDbContext;
    }
    
    private static DbContextOptions<T> CreateDbContextOptions<T>() where T : DbContext
    {
        DbContextOptions<T> optionsBuilder = new DbContextOptionsBuilder<T>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return optionsBuilder;
    }
}
