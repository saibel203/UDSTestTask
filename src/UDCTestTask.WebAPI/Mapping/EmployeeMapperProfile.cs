using AutoMapper;
using UDCTestTask.Core.DTOModels.EmployeeDTOs;
using UDCTestTask.Core.Models;
using UDCTestTask.WebAPI.InputModels;

namespace UDCTestTask.WebAPI.Mapping;

public class EmployeeMapperProfile : Profile
{
    public EmployeeMapperProfile()
    {
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<CreateEmployeeInputModel, CreateEmployeeDto>();

        CreateMap<RefreshEmployeeDto, Employee>();
        CreateMap<RefreshEmployeeInputModel, RefreshEmployeeDto>();
    }
}
