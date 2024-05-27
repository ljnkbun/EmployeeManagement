using Application.Models.Employees;
using Application.Parameters;
using AutoMapper;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

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

        public GetEmployeesQueryHandler(IMapper mapper,
            IEmployeeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PagedResponse<IReadOnlyList<EmployeeModel>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<EmployeeParameter>(request);
            return await _repository.GetModelPagedReponseAsync<EmployeeParameter, EmployeeModel>(validFilter);
        }
    }
}
