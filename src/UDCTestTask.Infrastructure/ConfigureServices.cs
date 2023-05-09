using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UDCTestTask.Infrastructure.ConfigurationHelpers;

namespace UDCTestTask.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextConfiguration(configuration);
        
        return services;
    }
}
