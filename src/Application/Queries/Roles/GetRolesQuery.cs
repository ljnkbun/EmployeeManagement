using Application.Models.Roles;
using Application.Parameters.Roles;
using AutoMapper;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Queries.Roles
{
    public class GetRolesQuery : IRequest<PagedResponse<IReadOnlyList<RoleModel>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? OrderBy { get; set; }
    }

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PagedResponse<IReadOnlyList<RoleModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repository;

        public GetRolesQueryHandler(IMapper mapper,
            IRoleRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PagedResponse<IReadOnlyList<RoleModel>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<RoleParameter>(request);
            return await _repository.GetModelPagedReponseAsync<RoleParameter, RoleModel>(validFilter);
        }
    }
}
