using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Commands.Roles
{
    public class UpdateRoleCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool IsDel { set; get; } = false;
    }
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Response<int>>
    {
        private readonly IRoleRepository _repository;
        public UpdateRoleCommandHandler(IRoleRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<int>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.Id);

            if (entity == null) throw new ApiException($"Role Not Found.");

            entity.Name = command.Name!;
            entity.Code = command.Code!;
            entity.IsDel = command.IsDel;

            await _repository.UpdateAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
