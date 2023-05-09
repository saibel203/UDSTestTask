using UDCTestTask.Core.DTOModels.EmployeeDTOs;
using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Core.Models;
using UDCTestTask.Core.Responses.Repositories;

namespace UDCTestTask.Core.Interfaces.Repositories.Main;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<EmployeeRepositoryResponse> GetAllEmployeesAsync();
    Task<EmployeeRepositoryResponse> GetEmployeeByIdAsync(int employeeId);
    Task<EmployeeRepositoryResponse> CreateEmployeeAsync(CreateEmployeeDto? employeeDto);
    Task<EmployeeRepositoryResponse> RemoveEmployeeAsync(int employeeId);
    Task<EmployeeRepositoryResponse> RefreshEmployeeDataAsync(RefreshEmployeeDto? employeeDto);
}