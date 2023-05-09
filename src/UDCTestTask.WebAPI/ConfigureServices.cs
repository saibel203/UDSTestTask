using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Data.Repositories.Based;
using UDCTestTask.Infrastructure.DatabaseContext;
using UDCTestTask.WebAPI.Mapping;

namespace UDCTestTask.WebAPI;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(EmployeeMapperProfile));
        
        services.AddScoped<SeedDbContext>();
        
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        
        services.AddControllers();
        
        return services;
    }
}
