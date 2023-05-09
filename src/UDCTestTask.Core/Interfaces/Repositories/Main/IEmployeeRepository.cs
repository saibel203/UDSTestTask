using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Core.Models;

namespace UDCTestTask.Core.Interfaces.Repositories.Main;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int employeeId);
    Task<Employee> CreateEmployeeAsync(Employee? employee);
    Task<Employee> RemoveEmployeeAsync(int employeeId);
    Task<Employee> RefreshEmployeeDataAsync(int employeeId, Employee? employee);
}