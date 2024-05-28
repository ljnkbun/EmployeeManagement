using Core.Exceptions;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Queries.Employees
{
    public class GetEmployeeQuery : IRequest<Response<Employee>>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, Response<Employee>>
    {
        private readonly IEmployeeRepository _repository;
        public GetEmployeeQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<Employee>> Handle(GetEmployeeQuery query, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null) throw new ApiException($"Employees Not Found (Id:{query.Id}).");
            return new Response<Employee>(entity);
        }
    }
}
