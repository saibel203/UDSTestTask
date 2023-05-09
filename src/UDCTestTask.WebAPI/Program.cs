using UDCTestTask.Infrastructure;
using UDCTestTask.Infrastructure.DatabaseContext;
using UDCTestTask.WebAPI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddApiServices();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    SeedDbContext init = scope.ServiceProvider.GetRequiredService<SeedDbContext>();
    await init.InitialiseDatabaseAsync();
    await init.SeedDataAsync();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
