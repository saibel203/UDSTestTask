using Microsoft.EntityFrameworkCore;
using UDCTestTask.Core.Models;
using UDCTestTask.Infrastructure.DatabaseContext;

namespace UDCTestTask.Shared;

public static class TaskApplicationDbContextFactory
{
    public static TaskApplicationDbContext CreateDatabaseWithData()
    {
        DbContextOptions<TaskApplicationDbContext> applicationContextOptions =
            new DbContextOptionsBuilder<TaskApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

        TaskApplicationDbContext applicationDbContext = new TaskApplicationDbContext(applicationContextOptions);
        applicationDbContext.Database.EnsureCreated();

        IEnumerable<Employee> employees = new List<Employee>
        {
            new()
            {
                EmployeeId = 1,
                FirstName = "First name",
                LastName = "Last name",
                Gender = "Male",
                City = "City"
            },
            new()
            {
                EmployeeId = 2,
                FirstName = "First name",
                LastName = "Last name",
                Gender = "Male",
                City = "City"
            }
        };

        applicationDbContext.Employees.AddRangeAsync(employees);
        applicationDbContext.SaveChangesAsync();

        return applicationDbContext;
    }

    public static void Destroy(TaskApplicationDbContext applicationDbContext)
    {
        applicationDbContext.Database.EnsureDeleted();
        applicationDbContext.Dispose();
    }
}
