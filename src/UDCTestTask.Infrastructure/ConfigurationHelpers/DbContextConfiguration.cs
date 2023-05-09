using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UDCTestTask.Infrastructure.DatabaseContext;

namespace UDCTestTask.Infrastructure.ConfigurationHelpers;

public static class DbContextConfiguration
{
    public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, 
        IConfiguration configuration)
    {
        const string applicationDbContext = "DefaultApplicationDbConnectionString";
        string? defaultApplicationDbConnectionString = configuration.GetConnectionString(applicationDbContext);
        
        services.AddDbContext<TaskApplicationDbContext>(options => 
            options.UseSqlServer(defaultApplicationDbConnectionString));

        return services;
    }
}
