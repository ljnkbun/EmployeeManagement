using AutoMapper;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Commands.Roles
{
    public class CreateRoleCommand : IRequest<Response<int>>
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repository;

        public CreateRoleCommandHandler(IMapper mapper,
            IRoleRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Role>(request);
            await _repository.AddRoleAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
