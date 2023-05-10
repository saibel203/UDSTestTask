using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Data.Repositories.Based;
using UDCTestTask.Infrastructure.DatabaseContext;
using UDCTestTask.WebAPI.Mapping;

namespace UDCTestTask.WebAPI;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        const string allowOrigins = "_angularClientOrigins";
    
        services.AddAutoMapper(typeof(EmployeeMapperProfile));
        
        services.AddScoped<SeedDbContext>();
        
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddCors(options =>
        {
            options.AddPolicy(name: allowOrigins, builder =>
            {
                builder.WithOrigins(configuration["ApplicationSettings:ClientWebPath"]!)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        services.AddControllers();
        
        return services;
    }
}
