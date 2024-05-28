using Application.Commands.Roles;
using Application.Models.Roles;
using Application.Parameters.Roles;
using Application.Queries.Roles;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<CreateRoleCommand, Role>();
            CreateMap<GetRolesQuery, RoleParameter>();
        }
    }
}
