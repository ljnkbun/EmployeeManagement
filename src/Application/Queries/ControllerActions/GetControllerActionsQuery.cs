using Application.Models.ControllerActions;
using Application.Parameters.ControllerActions;
using Application.Parameters.Employees;
using AutoMapper;
using Core.Models.Response;
using Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Queries.ControllerActions
{
    public class GetControllerActionsQuery : IRequest<PagedResponse<IReadOnlyList<ControllerActionModel>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; } = -1;

        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? OrderBy { get; set; }
    }

    public class GetControllerActionsQueryHandler : IRequestHandler<GetControllerActionsQuery, PagedResponse<IReadOnlyList<ControllerActionModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IControllerActionRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetControllerActionsQueryHandler(IMapper mapper,
            IControllerActionRepository repository
            , IEmployeeRepository employeeRepository
            , IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResponse<IReadOnlyList<ControllerActionModel>>> Handle(GetControllerActionsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ControllerActionParameter>(request);

            return await _repository.GetModelPagedReponseAsync<ControllerActionParameter, ControllerActionModel>(validFilter);
        }
    }
}
