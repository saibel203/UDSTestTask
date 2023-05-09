using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UDCTestTask.Core.DTOModels.EmployeeDTOs;
using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Core.Models;
using UDCTestTask.Core.Models.Auxiliary;
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
        ApiError error = new ApiError();

        if (!allEmployeesResult.IsSuccess)
        {
            error.ErrorCode = BadRequest().StatusCode;
            error.ErrorMessage = allEmployeesResult.Message;
            return BadRequest(error);
        }

        IEnumerable<Employee> employees = allEmployeesResult.Employees;

        return Ok(employees);
    }

    [HttpGet("getById/{id:int}")] // /api/employee/getById/{id}
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        EmployeeRepositoryResponse getEmployeeResult =
            await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(id);
        ApiError error = new ApiError();

        if (!getEmployeeResult.IsSuccess)
        {
            error.ErrorCode = NotFound().StatusCode;
            error.ErrorMessage = getEmployeeResult.Message;
            return NotFound(error);
        }

        Employee employee = getEmployeeResult.Employee;

        return Ok(employee);
    }

    [HttpPost("new")] // /api/employee/new
    public async Task<IActionResult> CreateEmployee(CreateEmployeeInputModel employeeInputModel)
    {
        CreateEmployeeDto createEmployeeDto = _mapper.Map<CreateEmployeeDto>(employeeInputModel);
        ApiError error = new ApiError();

        EmployeeRepositoryResponse createEmployeeResult =
            await _unitOfWork.EmployeeRepository.CreateEmployeeAsync(createEmployeeDto);

        if (!createEmployeeResult.IsSuccess)
        {
            error.ErrorCode = BadRequest().StatusCode;
            error.ErrorMessage = createEmployeeResult.Message;
            return BadRequest(error);
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
        ApiError error = new ApiError();

        if (!removeEmployeeResult.IsSuccess)
        {
            error.ErrorCode = NotFound().StatusCode;
            error.ErrorMessage = removeEmployeeResult.Message;
            return NotFound(error);
        }

        return Ok();
    }

    [HttpPut("refresh")] // /api/employee/refresh
    public async Task<IActionResult> RefreshEmployee(RefreshEmployeeInputModel refreshEmployeeInputModel)
    {
        RefreshEmployeeDto refreshEmployeeDto = _mapper.Map<RefreshEmployeeDto>(refreshEmployeeInputModel);
        ApiError error = new ApiError();

        EmployeeRepositoryResponse refreshEmployeeResult =
            await _unitOfWork.EmployeeRepository.RefreshEmployeeDataAsync(refreshEmployeeDto);

        if (!refreshEmployeeResult.IsSuccess)
        {
            error.ErrorCode = BadRequest().StatusCode;
            error.ErrorMessage = refreshEmployeeResult.Message;
            return BadRequest(error);
        }

        Employee employee = refreshEmployeeResult.Employee;

        return Ok(employee);
    }
}