using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UDCTestTask.Core.DTOModels.EmployeeDTOs;
using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Core.Models;
using UDCTestTask.Core.Responses.Repositories;
using UDCTestTask.WebAPI.InputModels;

namespace UDCTestTask.WebAPI.Controllers;

public class EmployeeController : ApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("all")] // /api/employee/all
    public async Task<IActionResult> AllEmployees()
    {
        EmployeeRepositoryResponse allEmployeesResult =
            await _unitOfWork.EmployeeRepository.GetAllEmployeesAsync();

        if (!allEmployeesResult.IsSuccess)
        {
            return BadRequest(allEmployeesResult);
        }

        IEnumerable<Employee> employees = allEmployeesResult.Employees;

        return Ok(employees);
    }

    [HttpGet("getById/{id:int}")] // /api/employee/getById/{id}
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        EmployeeRepositoryResponse getEmployeeResult =
            await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(id);

        if (!getEmployeeResult.IsSuccess)
        {
            return NotFound(getEmployeeResult);
        }

        Employee employee = getEmployeeResult.Employee;

        return Ok(employee);
    }

    [HttpPost("new")] // /api/employee/new
    public async Task<IActionResult> CreateEmployee(CreateEmployeeInputModel employeeInputModel)
    {
        CreateEmployeeDto createEmployeeDto = _mapper.Map<CreateEmployeeDto>(employeeInputModel);

        EmployeeRepositoryResponse createEmployeeResult =
            await _unitOfWork.EmployeeRepository.CreateEmployeeAsync(createEmployeeDto);

        if (!createEmployeeResult.IsSuccess)
        {
            return BadRequest(createEmployeeResult);
        }

        Employee employee = createEmployeeResult.Employee;

        return CreatedAtAction(nameof(GetEmployeeById), new {id = createEmployeeDto.EmployeeId},
            employee);
    }

    [HttpDelete("remove/{id:int}")] // /api/employee/remove/{id}
    public async Task<IActionResult> RemoveEmployee(int id)
    {
        EmployeeRepositoryResponse removeEmployeeResult =
            await _unitOfWork.EmployeeRepository.RemoveEmployeeAsync(id);

        if (!removeEmployeeResult.IsSuccess)
        {
            return NotFound(removeEmployeeResult);
        }

        return Ok();
    }

    [HttpPut("refresh")] // /api/employee/refresh
    public async Task<IActionResult> RefreshEmployee(RefreshEmployeeInputModel refreshEmployeeInputModel)
    {
        RefreshEmployeeDto refreshEmployeeDto = _mapper.Map<RefreshEmployeeDto>(refreshEmployeeInputModel);

        EmployeeRepositoryResponse refreshEmployeeResult =
            await _unitOfWork.EmployeeRepository.RefreshEmployeeDataAsync(refreshEmployeeDto);

        if (!refreshEmployeeResult.IsSuccess)
        {
            return BadRequest(refreshEmployeeResult);
        }

        Employee employee = refreshEmployeeResult.Employee;

        return Ok(employee);
    }
}