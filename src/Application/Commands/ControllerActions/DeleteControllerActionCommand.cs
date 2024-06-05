using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Commands.ControllerActions
{
    public class DeleteControllerActionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteControllerActionCommandHandler : IRequestHandler<DeleteControllerActionCommand, Response<int>>
    {
        private readonly IControllerActionRepository _repository;

        public DeleteControllerActionCommandHandler(IControllerActionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(DeleteControllerActionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.Id) ?? throw new ApiException($"ControllerAction Not Found (Id:{command.Id}).");
            var rs = await _repository.DeleteAsync(entity);
            if (!rs) throw new ApiException($"ControllerAction (Id:{command.Id}) Cant be Deleted ");
            return new Response<int>(entity.Id);
        }
    }
}
