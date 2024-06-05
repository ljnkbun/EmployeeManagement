using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Commands.ControllerActions
{
    public class UpdateControllerActionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string? Code { get; set; } 
        public string? Name { get; set; } 
        public string? Controller { get; set; } 
        public string? Action { get; set; } 
    }
    public class UpdateControllerActionCommandHandler : IRequestHandler<UpdateControllerActionCommand, Response<int>>
    {
        private readonly IControllerActionRepository _repository;
        public UpdateControllerActionCommandHandler(IControllerActionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<int>> Handle(UpdateControllerActionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.Id);

            if (entity == null) throw new ApiException($"ControllerAction Not Found.");

            entity.Name = command.Name!;
            entity.Code = command.Code!;
            entity.Controller = command.Controller!;
            entity.Action = command.Action!;

            await _repository.UpdateAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
