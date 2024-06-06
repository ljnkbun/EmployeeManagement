using Core.Exceptions;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Commands.Roles
{
    public class UpdateRoleCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int[]? ControllerActions { get; set; }
    }
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Response<int>>
    {
        private readonly IRoleRepository _repository;
        private readonly IRoleActionRepository _roleActionRepository;
        private readonly IControllerActionRepository _controllerActionRepository;

        public UpdateRoleCommandHandler(IRoleRepository repository
            , IRoleActionRepository roleActionRepository
            , IControllerActionRepository controllerActionRepository
            )
        {
            _repository = repository;
            _roleActionRepository = roleActionRepository;
            _controllerActionRepository = controllerActionRepository;
        }
        public async Task<Response<int>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetDeepByIdAsync(command.Id);

            if (entity == null) throw new ApiException($"Role Not Found.");
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
            entity.Name = command.Name!;
            entity.Code = command.Code!;

            await _repository.UpdateRoleAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
