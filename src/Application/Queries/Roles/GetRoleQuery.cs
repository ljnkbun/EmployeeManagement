using Core.Exceptions;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Queries.Roles
{
    public class GetRoleQuery : IRequest<Response<Role>>
    {
        public int Id { get; set; }
    }

    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Response<Role>>
    {
        private readonly IRoleRepository _repository;

        public GetRoleQueryHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<Role>> Handle(GetRoleQuery query, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null) throw new ApiException($"Roles Not Found (Id:{query.Id}).");
            return new Response<Role>(entity);
        }
    }
}
