using UDCTestTask.Core.Models;

namespace UDCTestTask.Core.Responses.Repositories;

public class EmployeeRepositoryResponse : BaseResponse
{
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>(16);
    public Employee Employee { get; set; } = new();
}
