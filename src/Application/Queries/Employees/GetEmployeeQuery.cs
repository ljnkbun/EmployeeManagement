using Application.Models.Employees;
using AutoMapper;
using Core.Exceptions;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Queries.Employees
{
    public class GetEmployeeQuery : IRequest<Response<EmployeeModel>>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Response<EmployeeModel>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandler(IEmployeeRepository repository
            , IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<EmployeeModel>> Handle(GetEmployeeQuery query, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetDeepByIdAsync(query.Id);
            if (entity == null) throw new ApiException($"Employees Not Found (Id:{query.Id}).");
            var rs = _mapper.Map<EmployeeModel>(entity);
            return new Response<EmployeeModel>(rs);
        }
    }
}
