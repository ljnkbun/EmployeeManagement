using AutoMapper;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Commands.Roles
{
    public class CreateRoleCommand : IRequest<Response<int>>
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public int[]? ControllerActions { get; set; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repository;
        private readonly IControllerActionRepository _controllerActionRepository;

        public CreateRoleCommandHandler(IMapper mapper,
            IRoleRepository repository
            , IControllerActionRepository controllerActionRepository
            )
        {
            _repository = repository;
            _controllerActionRepository = controllerActionRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Role>(command);

            if (command.ControllerActions != null)
            {
                var controllerAction = await _controllerActionRepository.GetByIdsAsync(command.ControllerActions);
                entity.RoleActions = new List<RoleAction>();
                foreach (var action in controllerAction)
                {
                    entity.RoleActions!.Add(new()
                    {
                        Action = action.Action,
                        Controller = action.Controller,
                        RoleId = entity.Id,
                        ControllerActionId = action.Id
                    });
                }
            }
            await _repository.AddRoleAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
