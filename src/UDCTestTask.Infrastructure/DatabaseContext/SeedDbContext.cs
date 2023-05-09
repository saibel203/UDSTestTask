using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UDCTestTask.Core.Models;

namespace UDCTestTask.Infrastructure.DatabaseContext;

public class SeedDbContext
{
    private readonly ILogger<SeedDbContext> _logger;
    private readonly TaskApplicationDbContext _applicationDbContext;

    public SeedDbContext(ILogger<SeedDbContext> logger, TaskApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }
    
    public async Task InitialiseDatabaseAsync()
    {
        try
        {
            if (_applicationDbContext.Database.IsSqlServer())
            {
                await _applicationDbContext.Database.MigrateAsync();
                _logger.LogInformation("Database udc_software_test_task created");
            }

            if (_applicationDbContext.Database.IsSqlServer())
            {
                await _applicationDbContext.Database.MigrateAsync();
                _logger.LogInformation("Database udc_software_test_task created");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while trying to initialize the database");
            throw;
        }
    }
    
    public async Task SeedDataAsync()
    {
        try
        {
            await TrySeedDataAsync();
            _logger.LogInformation("The data has been successfully added to the databases");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while trying to seed the database");
            throw;
        }
    }

    public async Task TrySeedDataAsync()
    {
        // Seed Employee 
        const string maleGender = "Male";
        const string femaleGender = "Female";
        
        object[] employees =
        {
            new Employee
            {
                FirstName = "Maxim",
                LastName = "Dydar",
                Gender = maleGender,
                City = "Odessa"
            },
            new Employee
            {
                FirstName = "Oksana ",
                LastName = "Zabuzko",
                Gender = femaleGender,
                City = "Lviv"
            },
            new Employee
            {
                FirstName = "Anatoliy",
                LastName = "Yakimenki",
                Gender = maleGender,
                City = "Kyiv"
            },
            new Employee
            {
                FirstName = "Anton",
                LastName = "Manan",
                Gender = maleGender,
                City = "Rivne"
            },
            new Employee
            {
                FirstName = "Anastasia",
                LastName = "Bachinska",
                Gender = femaleGender,
                City = "Ternopil"
            }
        };

        if (!await _applicationDbContext.Employees.AnyAsync())
        {
            await _applicationDbContext.AddRangeAsync(employees);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
