using Core.Exceptions;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Queries.ControllerActions
{
    public class GetControllerActionQuery : IRequest<Response<ControllerAction>>
    {
        public int Id { get; set; }
    }

    public class GetControllerActionQueryHandler : IRequestHandler<GetControllerActionQuery, Response<ControllerAction>>
    {
        private readonly IControllerActionRepository _repository;

        public GetControllerActionQueryHandler(IControllerActionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ControllerAction>> Handle(GetControllerActionQuery query, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null) throw new ApiException($"ControllerActions Not Found (Id:{query.Id}).");
            return new Response<ControllerAction>(entity);
        }
    }
}
