using System.ComponentModel.DataAnnotations;

namespace UDCTestTask.WebAPI.InputModels;

public class CreateEmployeeInputModel
{
    [Required(ErrorMessage = "The 'First name' field is mandatory")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "The length of the field should be from 5 to 20 characters")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "The 'Last name' field is mandatory")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "The length of the field should be from 5 to 20 characters")]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "The 'Gender' field is mandatory")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "The length of the field should be from 5 to 20 characters")]
    public string Gender { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "The 'City' field is mandatory")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "The length of the field should be from 5 to 50 characters")]
    public string City { get; set; } = string.Empty;
}
