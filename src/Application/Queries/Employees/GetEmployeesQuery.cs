using Application.Models.Employees;
using Application.Parameters.Employees;
using AutoMapper;
using Core.Models.Response;
using Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Queries.Employees
{
    public class GetEmployeesQuery : IRequest<PagedResponse<IReadOnlyList<EmployeeModel>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? OrderBy { get; set; }
    }

    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PagedResponse<IReadOnlyList<EmployeeModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetEmployeesQueryHandler(IMapper mapper
           , IEmployeeRepository repository
            , IEmployeeRepository employeeRepository
            , IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            _employeeRepository = employeeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PagedResponse<IReadOnlyList<EmployeeModel>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var curUserId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "id")!.Value;
            var curUser = await _employeeRepository.GetByIdAsync(int.Parse(curUserId));

            var validFilter = _mapper.Map<EmployeeParameter>(request);
#if DEBUG
            //ADMIN can see all
            if (curUserId != "1")
            {
                validFilter.DivisionId = curUser.DivisionId;
            }
#endif
            return await _repository.GetModelPagedReponseAsync<EmployeeParameter, EmployeeModel>(validFilter);
        }
    }
}
