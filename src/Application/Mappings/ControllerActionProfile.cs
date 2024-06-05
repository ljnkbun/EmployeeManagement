using Application.Commands.ControllerActions;
using Application.Models.ControllerActions;
using Application.Parameters.ControllerActions;
using Application.Queries.ControllerActions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ControllerActionProfile : Profile
    {
        public ControllerActionProfile()
        {
            CreateMap<ControllerAction, ControllerActionModel>().ReverseMap();
            CreateMap<CreateControllerActionCommand, ControllerAction>();
            CreateMap<GetControllerActionsQuery, ControllerActionParameter>();
        }
    }
}
