using Application.Models.RoleActions;
using Application.Parameters.RoleActions;
using AutoMapper;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Queries.RoleActions
{
    public class GetRoleActionsQuery : IRequest<PagedResponse<IReadOnlyList<RoleActionModel>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public string? Controller { get; set; } 
        public string? Action { get; set; } 
        public int RoleId { get; set; }

        public string? OrderBy { get; set; }
    }

    public class GetRoleActionsQueryHandler : IRequestHandler<GetRoleActionsQuery, PagedResponse<IReadOnlyList<RoleActionModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleActionRepository _repository;

        public GetRoleActionsQueryHandler(IMapper mapper,
            IRoleActionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PagedResponse<IReadOnlyList<RoleActionModel>>> Handle(GetRoleActionsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<RoleActionParameter>(request);
            return await _repository.GetModelPagedReponseAsync<RoleActionParameter, RoleActionModel>(validFilter);
        }
    }
}
