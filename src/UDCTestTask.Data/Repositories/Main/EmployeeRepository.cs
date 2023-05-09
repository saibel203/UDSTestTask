using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UDCTestTask.Core.Interfaces.Repositories.Main;
using UDCTestTask.Core.Models;
using UDCTestTask.Data.Repositories.Based;
using UDCTestTask.Infrastructure.DatabaseContext;

namespace UDCTestTask.Data.Repositories.Main;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(TaskApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
    {
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        try
        {
            IEnumerable<Employee> allEmployees = await DbSet.ToListAsync();
            return allEmployees;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "");
            return new List<Employee>();
        }
    }

    public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
        try
        {
            Employee? employee = await DbSet.FirstOrDefaultAsync(employeeData => employeeData.EmployeeId == employeeId);
            
            if (employee is null)
                return new Employee();

            return employee;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "");
            return new Employee();
        }
    }
    
    public async Task<Employee> CreateEmployeeAsync(Employee? employee)
    {
        try
        {
            if (employee is null)
                return new Employee();

            await DbSet.AddAsync(employee);
            await DbContext.SaveChangesAsync();

            return employee;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "");
            return new Employee();
        }
    }

    public async Task<Employee> RemoveEmployeeAsync(int employeeId)
    {
        try
        {
            Employee? employee = await DbSet.FirstOrDefaultAsync(employeeData => employeeData.EmployeeId == employeeId);
            
            if (employee is null)
                return new Employee();

            DbSet.Remove(employee);
            await DbContext.SaveChangesAsync();

            return employee;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "");
            return new Employee();
        }
    }

    public async Task<Employee> RefreshEmployeeDataAsync(int employeeId, Employee? employee)
    {
        try
        {
            if (employee is null)
                return new Employee();

            if (employee.EmployeeId != employeeId)
                return new Employee();

            DbSet.Update(employee);
            await DbContext.SaveChangesAsync();
            
            return employee;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "");
            return new Employee();
        }
    }
}