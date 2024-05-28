using Application.Commands.Employees;
using Application.Models.Employees;
using Application.Parameters.Employees;
using Application.Queries.Employees;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<CreateEmployeeCommand, Employee>();
            CreateMap<GetEmployeesQuery, EmployeeParameter>();
        }
    }
}
