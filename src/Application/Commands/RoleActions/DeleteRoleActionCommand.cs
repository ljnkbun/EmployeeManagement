using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Commands.RoleActions
{
    public class DeleteRoleActionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteRoleActionCommandHandler : IRequestHandler<DeleteRoleActionCommand, Response<int>>
    {
        private readonly IRoleActionRepository _repository;

        public DeleteRoleActionCommandHandler(IRoleActionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(DeleteRoleActionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.Id) ?? throw new ApiException($"RoleAction Not Found (Id:{command.Id}).");
            await _repository.DeleteAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
