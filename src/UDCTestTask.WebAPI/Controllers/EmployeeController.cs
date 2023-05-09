using Microsoft.AspNetCore.Mvc;
using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Core.Models;

namespace UDCTestTask.WebAPI.Controllers;

public class EmployeeController : ApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("all")] // /api/employee/all
    public async Task<IActionResult> AllEmployees() 
    {
        var test = await _unitOfWork.EmployeeRepository.GetAllEmployeesAsync();
        return Ok(test);
    }

    [HttpGet("getById/{id:int}")] // /api/employee/getById/{id}
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var test = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(id);
        return Ok(test);
    }

    [HttpPost("new")] // /api/employee/new
    public async Task<IActionResult> CreateEmployee(Employee employee)
    {
        var test = await _unitOfWork.EmployeeRepository.CreateEmployeeAsync(employee);
        return Ok(test);
    }

    [HttpDelete("remove/{id:int}")] // /api/employee/remove/{id}
    public async Task<IActionResult> RemoveEmployee(int id)
    {
        var test = await _unitOfWork.EmployeeRepository.RemoveEmployeeAsync(id);
        return Ok(test);
    }

    [HttpPut("refresh/{id:int}")] // /api/employee/refresh/{id}
    public async Task<IActionResult> RefreshEmployee(int id, Employee employee)
    {
        var test = await _unitOfWork.EmployeeRepository.RefreshEmployeeDataAsync(id, employee);
        return Ok(test);
    }
}
