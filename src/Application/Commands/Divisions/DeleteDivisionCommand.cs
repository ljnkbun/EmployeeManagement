using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Commands.Divisions
{
    public class DeleteDivisionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteDivisionCommandHandler : IRequestHandler<DeleteDivisionCommand, Response<int>>
    {
        private readonly IDivisionRepository _repository;

        public DeleteDivisionCommandHandler(IDivisionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(DeleteDivisionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.Id) ?? throw new ApiException($"Division Not Found (Id:{command.Id}).");
            var rs = await _repository.DeleteAsync(entity);
            if (!rs) throw new ApiException($"Division {entity.Name} Cant be Deleted because it's used");
            return new Response<int>(entity.Id);
        }
    }
}
