using AutoMapper;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Commands.RoleActions
{
    public class CreateRoleActionCommand : IRequest<Response<int>>
    {
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public int RoleId { get; set; }
    }

    public class CreateRoleActionCommandHandler : IRequestHandler<CreateRoleActionCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleActionRepository _repository;

        public CreateRoleActionCommandHandler(IMapper mapper,
            IRoleActionRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateRoleActionCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<RoleAction>(request);
            await _repository.AddAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
