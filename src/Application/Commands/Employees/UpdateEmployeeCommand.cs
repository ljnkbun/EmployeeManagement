using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Employees
{
    public class UpdateEmployeeCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public int DivisionId { get; set; }
        public int[]? RoleIds { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Response<int>>
    {
        private readonly IEmployeeRepository _repository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<int>> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.Id);

            if (entity == null) throw new ApiException($"Employee Not Found.");

            entity.Name = command.Name!;
            entity.Code = command.Code!;
            entity.Username = command.Username!;
            entity.Password = new PasswordHasher<object?>().HashPassword(null!, command.Password!);
            if (command.DivisionId > 0)
                entity.DivisionId = command.DivisionId;

            await _repository.UpdateEmployeeAsync(entity, command.RoleIds);
            return new Response<int>(entity.Id);
        }
    }
}
