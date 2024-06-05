using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Commands.Roles
{
    public class DeleteRoleCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Response<int>>
    {
        private readonly IRoleRepository _repository;

        public DeleteRoleCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 1)//ADMIN prevent delete
                throw new ApiException($"Role ADMIN couldnt be deleted");
            var entity = await _repository.GetByIdAsync(command.Id) ?? throw new ApiException($"Role Not Found (Id:{command.Id}).");
            await _repository.DeleteRoleAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
