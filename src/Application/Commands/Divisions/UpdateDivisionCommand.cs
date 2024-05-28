using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Commands.Divisions
{
    public class UpdateDivisionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsDel { set; get; } = false;
    }
    public class UpdateDivisionCommandHandler : IRequestHandler<UpdateDivisionCommand, Response<int>>
    {
        private readonly IDivisionRepository _repository;
        public UpdateDivisionCommandHandler(IDivisionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<int>> Handle(UpdateDivisionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.Id);

            if (entity == null) throw new ApiException($"Division Not Found.");

            entity.Name = command.Name!;
            entity.IsDel = command.IsDel;

            await _repository.UpdateAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
