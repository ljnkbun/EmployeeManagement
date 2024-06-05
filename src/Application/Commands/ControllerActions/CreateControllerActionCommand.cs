using AutoMapper;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Commands.ControllerActions
{
    public class CreateControllerActionCommand : IRequest<Response<int>>
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
    }

    public class CreateControllerActionCommandHandler : IRequestHandler<CreateControllerActionCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IControllerActionRepository _repository;

        public CreateControllerActionCommandHandler(IMapper mapper,
            IControllerActionRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateControllerActionCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ControllerAction>(request);
            await _repository.AddAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
