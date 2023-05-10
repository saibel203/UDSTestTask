using AutoMapper;
using UDCTestTask.Core.DTOModels.EmployeeDTOs;
using UDCTestTask.Core.Models;
using UDCTestTask.Core.Responses.Repositories;
using UDCTestTask.Data.Repositories.Main;
using UDCTestTask.Data.UnitTests.Helpers;
using UDCTestTask.Shared;
using UDCTestTask.WebAPI.Mapping;
using Xunit;

namespace UDCTestTask.Data.UnitTests.Repositories.Main;

public class EmployeeRepositoryTests : TestBase<EmployeeRepository>
{
    private const int ExpectedEmployeesInDatabase = 2;
    
    private readonly EmployeeRepository _employeeRepository;

    public EmployeeRepositoryTests()
    {
        EmployeeMapperProfile employeeProfile = new EmployeeMapperProfile();
        var configuration = new MapperConfiguration(cfg => 
            cfg.AddProfile(employeeProfile));
        IMapper mapper = new Mapper(configuration);
        
        _employeeRepository = new EmployeeRepository(ApplicationDbContext, Logger, mapper);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_EmployeesReceivedSuccessfully_ReturnSuccessResponse()
    {
        // Act
        EmployeeRepositoryResponse getAllEmployeesResponse =
            await _employeeRepository.GetAllEmployeesAsync();
        IEnumerable<Employee> allEmployees = getAllEmployeesResponse.Employees.ToList();
        int actualEmployeesCount = allEmployees.Count();
        bool isSuccess = getAllEmployeesResponse.IsSuccess;

        // Assert
        Assert.True(isSuccess);
        Assert.NotNull(allEmployees);
        Assert.NotEmpty(allEmployees);
        Assert.Equal(ExpectedEmployeesInDatabase, actualEmployeesCount);
        Assert.IsAssignableFrom<IEnumerable<Employee>>(allEmployees);
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_SuccessGetEmployeeData_ReturnSuccessResponse()
    {
        // Arrange
        const int employeeId = 1;

        // Act
        EmployeeRepositoryResponse getEmployeeResult =
            await _employeeRepository.GetEmployeeByIdAsync(employeeId);
        Employee employee = getEmployeeResult.Employee;
        int actualEmployeesCount = ApplicationDbContext.Employees.Count();
        bool isSuccess = getEmployeeResult.IsSuccess;
        
        // Assert
        Assert.True(isSuccess);
        Assert.NotNull(employee);
        Assert.Equal(ExpectedEmployeesInDatabase, actualEmployeesCount);
        Assert.IsAssignableFrom<Employee>(employee);
    }
    
    [Fact]
    public async Task GetEmployeeByIdAsync_EmployeeWithIdNotFound_ReturnErrorResponse()
    {
        // Arrange
        const int getEmployeeId = 3;
        const int expectedResultEmployeeId = 0;

        // Act
        EmployeeRepositoryResponse getEmployeeResult =
            await _employeeRepository.GetEmployeeByIdAsync(getEmployeeId);
        Employee employee = getEmployeeResult.Employee;
        int actualEmployeesCount = ApplicationDbContext.Employees.Count();
        bool isSuccess = getEmployeeResult.IsSuccess;
        int employeeId = employee.EmployeeId;
        
        // Assert
        Assert.False(isSuccess);
        Assert.Equal(expectedResultEmployeeId, employeeId);
        Assert.Equal(ExpectedEmployeesInDatabase, actualEmployeesCount);
    }

    [Theory, AutoDomainData]
    public async Task CreateEmployeeAsync_SuccessCreateNewEmployee_ReturnSuccessResponse(
        CreateEmployeeDto createEmployeeDto)
    {
        // Arrange
        const int expectedNewEmployeeId = 3;
        const int expectedEmployeesCount = 3;
        createEmployeeDto.EmployeeId = expectedNewEmployeeId;

        // Act
        EmployeeRepositoryResponse createUserResult =
            await _employeeRepository.CreateEmployeeAsync(createEmployeeDto);
        Employee employee = createUserResult.Employee;
        int actualEmployeesCount = ApplicationDbContext.Employees.Count();
        bool isSuccess = createUserResult.IsSuccess;
        int employeeId = employee.EmployeeId;

        // Assert
        Assert.True(isSuccess);
        Assert.Equal(expectedEmployeesCount, actualEmployeesCount);
        Assert.Equal(expectedNewEmployeeId, employeeId);
    }
    
    [Fact]
    public async Task CreateEmployeeAsync_ErrorCreateNewEmployee_ReturnErrorResponse()
    {
        // Arrange
        const int expectedNewEmployeeId = 0;
        CreateEmployeeDto? createEmployeeDto = null;

        // Act
        EmployeeRepositoryResponse createUserResult =
            await _employeeRepository.CreateEmployeeAsync(createEmployeeDto);
        Employee employee = createUserResult.Employee;
        int actualEmployeesCount = ApplicationDbContext.Employees.Count();
        bool isSuccess = createUserResult.IsSuccess;
        int employeeId = employee.EmployeeId;

        // Assert
        Assert.False(isSuccess);
        Assert.Equal(ExpectedEmployeesInDatabase, actualEmployeesCount);
        Assert.Equal(expectedNewEmployeeId, employeeId);
    }
    
    [Fact]
    public async Task RemoveEmployeeAsync_SuccessRemoveEmployee_ReturnSuccessResponse()
    {
        // Arrange
        const int removeEmployeeId = 1;
        const int expectedEmployeesCountAfterRemove = 1;

        // Act
        EmployeeRepositoryResponse getEmployeeResult =
            await _employeeRepository.RemoveEmployeeAsync(removeEmployeeId);
        int actualEmployeesCount = ApplicationDbContext.Employees.Count();
        bool isSuccess = getEmployeeResult.IsSuccess;
        
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(expectedEmployeesCountAfterRemove, actualEmployeesCount);
    }
    
    [Fact]
    public async Task RemoveEmployeeAsync_EmployeeWithIdNotFound_ReturnErrorResponse()
    {
        // Arrange
        const int removeEmployeeId = 3;
        const int expectedEmployeesCountAfterRemove = 2;

        // Act
        EmployeeRepositoryResponse getEmployeeResult =
            await _employeeRepository.RemoveEmployeeAsync(removeEmployeeId);
        int actualEmployeesCount = ApplicationDbContext.Employees.Count();
        bool isSuccess = getEmployeeResult.IsSuccess;
        
        // Assert
        Assert.False(isSuccess);
        Assert.Equal(expectedEmployeesCountAfterRemove, actualEmployeesCount);
    }
}
