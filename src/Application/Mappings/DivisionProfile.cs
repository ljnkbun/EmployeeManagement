using Application.Commands.Divisions;
using Application.Models.Divisions;
using Application.Parameters.Divisions;
using Application.Queries.Divisions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DivisionProfile : Profile
    {
        public DivisionProfile()
        {
            CreateMap<Division, DivisionModel>().ReverseMap();
            CreateMap<CreateDivisionCommand, Division>();
            CreateMap<GetDivisionsQuery, DivisionParameter>();
        }
    }
}
