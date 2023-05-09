namespace UDCTestTask.Core.DTOModels.EmployeeDTOs;

public class CreateEmployeeDto
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
