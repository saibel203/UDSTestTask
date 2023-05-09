using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UDCTestTask.Core.DTOModels.EmployeeDTOs;
using UDCTestTask.Core.Interfaces.Repositories.Main;
using UDCTestTask.Core.Models;
using UDCTestTask.Core.Responses.Repositories;
using UDCTestTask.Data.Repositories.Based;
using UDCTestTask.Infrastructure.DatabaseContext;

namespace UDCTestTask.Data.Repositories.Main;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    private readonly IMapper _mapper;

    public EmployeeRepository(TaskApplicationDbContext dbContext, ILogger logger, IMapper mapper) : base(dbContext,
        logger)
    {
        _mapper = mapper;
    }

    public async Task<EmployeeRepositoryResponse> GetAllEmployeesAsync()
    {
        try
        {
            IEnumerable<Employee> allEmployees = await DbSet.ToListAsync();

            return new EmployeeRepositoryResponse
            {
                Message = "All employees were successfully received",
                IsSuccess = true,
                Employees = allEmployees
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An unknown error occurred while attempting to retrieve data about all employees");
            return new EmployeeRepositoryResponse
            {
                Message = "An unknown error occurred while attempting to retrieve data about all employees",
                IsSuccess = false
            };
        }
    }

    public async Task<EmployeeRepositoryResponse> GetEmployeeByIdAsync(int employeeId)
    {
        try
        {
            Employee? employee = await DbSet.FirstOrDefaultAsync(employeeData => employeeData.EmployeeId == employeeId);

            if (employee is null)
                return new EmployeeRepositoryResponse
                {
                    Message = "The employee with the specified identifier was not found",
                    IsSuccess = false
                };

            return new EmployeeRepositoryResponse
            {
                Message = "The employee with the ID was successfully obtained",
                IsSuccess = true,
                Employee = employee
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An unknown error occurred while trying to retrieve employee data by id");
            return new EmployeeRepositoryResponse
            {
                Message = "An unknown error occurred while trying to retrieve employee data by id",
                IsSuccess = false
            };
        }
    }

    public async Task<EmployeeRepositoryResponse> CreateEmployeeAsync(CreateEmployeeDto? employeeDto)
    {
        try
        {
            if (employeeDto is null)
                return new EmployeeRepositoryResponse
                {
                    Message = "An unknown error occurred while trying to retrieve data about a new user",
                    IsSuccess = false
                };

            Employee employeeData = _mapper.Map<Employee>(employeeDto);

            await DbSet.AddAsync(employeeData);
            await DbContext.SaveChangesAsync();

            return new EmployeeRepositoryResponse
            {
                Message = "New user successfully created",
                IsSuccess = true,
                Employee = employeeData
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An unknown error occurred while trying to create a new employee");
            return new EmployeeRepositoryResponse
            {
                Message = "An unknown error occurred while trying to create a new employee",
                IsSuccess = false
            };
        }
    }

    public async Task<EmployeeRepositoryResponse> RemoveEmployeeAsync(int employeeId)
    {
        try
        {
            Employee? employee = await DbSet.FirstOrDefaultAsync(employeeData => employeeData.EmployeeId == employeeId);

            if (employee is null)
                return new EmployeeRepositoryResponse
                {
                    Message = "The employee with the specified identifier was not found",
                    IsSuccess = false
                };

            DbSet.Remove(employee);
            await DbContext.SaveChangesAsync();

            return new EmployeeRepositoryResponse
            {
                Message = "The user with this ID was successfully deleted",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An unknown error occurred while trying to delete the employee");
            return new EmployeeRepositoryResponse
            {
                Message = "An unknown error occurred while trying to delete the employee",
                IsSuccess = false
            };
        }
    }

    public async Task<EmployeeRepositoryResponse> RefreshEmployeeDataAsync(RefreshEmployeeDto? employeeDto)
    {
        try
        {
            if (employeeDto is null)
                return new EmployeeRepositoryResponse
                {
                    Message = "An unknown error occurred while trying to retrieve data about a user",
                    IsSuccess = false
                };

            Employee employee = _mapper.Map<Employee>(employeeDto);

            DbSet.Update(employee);
            await DbContext.SaveChangesAsync();

            return new EmployeeRepositoryResponse
            {
                Message = "User data has been refreshed successfully",
                IsSuccess = true,
                Employee = employee
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An unknown error occurred while trying to update employee data");
            return new EmployeeRepositoryResponse
            {
                Message = "An unknown error occurred while trying to update employee data",
                IsSuccess = false
            };
        }
    }
}