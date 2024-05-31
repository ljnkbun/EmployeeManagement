using Application.Commands.RoleActions;
using Application.Models.RoleActions;
using Application.Parameters.RoleActions;
using Application.Queries.RoleActions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class RoleActionProfile : Profile
    {
        public RoleActionProfile()
        {
            CreateMap<RoleAction, RoleActionModel>().ReverseMap();
            CreateMap<CreateRoleActionCommand, RoleAction>();
            CreateMap<GetRoleActionsQuery, RoleActionParameter>();
        }
    }
}
