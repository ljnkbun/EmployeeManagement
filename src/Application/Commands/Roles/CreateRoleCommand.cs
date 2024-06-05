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
        public int[]? RoleActions { get; set; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repository;
        private readonly IRoleActionRepository _roleActionRepository;

        public CreateRoleCommandHandler(IMapper mapper,
            IRoleRepository repository
            , IRoleActionRepository roleActionRepository
            )
        {
            _repository = repository;
            _roleActionRepository = roleActionRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Role>(request);
            if (request.RoleActions != null)
            {
                var roleAction = await _roleActionRepository.GetByIdsAsync(request.RoleActions);
                entity.RoleActions = roleAction;
            }
            await _repository.AddRoleAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
