using Application.Models.Roles;
using AutoMapper;
using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Queries.Roles
{
    public class GetRoleQuery : IRequest<Response<RoleModel>>
    {
        public int Id { get; set; }
    }

    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Response<RoleModel>>
    {
        private readonly IRoleRepository _repository;
        private readonly IControllerActionRepository _controllerActionRepository;
        private readonly IMapper _mapper;

        public GetRoleQueryHandler(IRoleRepository repository
            , IControllerActionRepository controllerActionRepository
            , IMapper mapper
            )
        {
            _repository = repository;
            _controllerActionRepository = controllerActionRepository;
            _mapper = mapper;
        }

        public async Task<Response<RoleModel>> Handle(GetRoleQuery query, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetDeepByIdAsync(query.Id);
            if (entity == null) throw new ApiException($"Roles Not Found (Id:{query.Id}).");

            var rs = _mapper.Map<RoleModel>(entity);
            var enityControllerActions = await _controllerActionRepository.GetByIdsAsync(entity.RoleActions?.Select(x => x.ControllerActionId).ToArray());
            rs.ControllerActions = enityControllerActions?.Select(x => x.Id).ToArray();

            return new Response<RoleModel>(rs);
        }
    }
}
