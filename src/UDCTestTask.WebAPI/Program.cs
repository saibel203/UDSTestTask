using UDCTestTask.Infrastructure;
using UDCTestTask.Infrastructure.DatabaseContext;
using UDCTestTask.WebAPI;
using UDCTestTask.WebAPI.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddApiServices();

WebApplication app = builder.Build();
IWebHostEnvironment environment = app.Environment;

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    SeedDbContext init = scope.ServiceProvider.GetRequiredService<SeedDbContext>();
    await init.InitialiseDatabaseAsync();
    await init.SeedDataAsync();
}

app.ConfigureExceptionHandler(environment);

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
