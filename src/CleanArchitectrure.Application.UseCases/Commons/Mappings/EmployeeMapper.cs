using AutoMapper;
using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Employees.Commands.CreateEmployeeCommand;
using CleanArchitectrure.Application.UseCases.Employees.Commands.UpdateEmployeeCommand;
using CleanArchitectrure.Domain.Entities;

namespace CleanArchitectrure.Application.UseCases.Commons.Mappings
{
    public class EmployeeMapper: Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, CreateEmployeeCommand>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeCommand>().ReverseMap();
        }
    }
}
