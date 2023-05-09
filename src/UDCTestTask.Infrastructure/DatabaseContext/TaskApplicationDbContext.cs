using Microsoft.EntityFrameworkCore;
using UDCTestTask.Core.Models;

namespace UDCTestTask.Infrastructure.DatabaseContext;

public class TaskApplicationDbContext : DbContext
{
    public TaskApplicationDbContext(DbContextOptions<TaskApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Employee>()
            .HasKey(b => b.EmployeeId);
    }
}