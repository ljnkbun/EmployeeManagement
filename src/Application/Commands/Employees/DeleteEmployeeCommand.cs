using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Commands.Employees
{
    public class DeleteEmployeeCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Response<int>>
    {
        private readonly IEmployeeRepository _repository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 1)//ADMIN prevent delete
                throw new ApiException($"Role ADMIN couldnt be deleted");
            var entity = await _repository.GetByIdAsync(command.Id) ?? throw new ApiException($"Employee Not Found (Id:{command.Id}).");
            await _repository.DeleteEmployeeAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
