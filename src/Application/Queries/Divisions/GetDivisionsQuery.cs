using Application.Models.Divisions;
using Application.Parameters.Divisions;
using Application.Parameters.Employees;
using AutoMapper;
using Core.Models.Response;
using Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Queries.Divisions
{
    public class GetDivisionsQuery : IRequest<PagedResponse<IReadOnlyList<DivisionModel>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? OrderBy { get; set; }
    }

    public class GetDivisionsQueryHandler : IRequestHandler<GetDivisionsQuery, PagedResponse<IReadOnlyList<DivisionModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IDivisionRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetDivisionsQueryHandler(IMapper mapper,
            IDivisionRepository repository
            , IEmployeeRepository employeeRepository
            , IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResponse<IReadOnlyList<DivisionModel>>> Handle(GetDivisionsQuery request, CancellationToken cancellationToken)
        {
            var curUserId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "id")!.Value;
            var curUser = await _employeeRepository.GetByIdAsync(int.Parse(curUserId));

            var validFilter = _mapper.Map<DivisionParameter>(request);
#if DEBUG
            //ADMIN can see all
            if (curUserId != "1")
            {
                validFilter.Id = curUser.DivisionId;
            }
#endif
            return await _repository.GetModelPagedReponseAsync<DivisionParameter, DivisionModel>(validFilter);
        }
    }
}
