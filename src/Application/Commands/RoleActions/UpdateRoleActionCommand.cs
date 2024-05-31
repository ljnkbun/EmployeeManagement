using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Commands.RoleActions
{
    public class UpdateRoleActionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public int RoleId { get; set; }
    }
    public class UpdateRoleActionCommandHandler : IRequestHandler<UpdateRoleActionCommand, Response<int>>
    {
        private readonly IRoleActionRepository _repository;
        public UpdateRoleActionCommandHandler(IRoleActionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<int>> Handle(UpdateRoleActionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.Id);

            if (entity == null) throw new ApiException($"RoleAction Not Found.");

            entity.RoleId = command.RoleId;
            entity.Controller = command.Controller;
            entity.Action = command.Action;

            await _repository.UpdateAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
