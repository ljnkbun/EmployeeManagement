﻿using Core.Exceptions;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Queries.RoleActions
{
    public class GetRoleActionQuery : IRequest<Response<RoleAction>>
    {
        public int Id { get; set; }
    }

    public class GetRoleActionQueryHandler : IRequestHandler<GetRoleActionQuery, Response<RoleAction>>
    {
        private readonly IRoleActionRepository _repository;

        public GetRoleActionQueryHandler(IRoleActionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<RoleAction>> Handle(GetRoleActionQuery query, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(query.Id);
            if (entity == null) throw new ApiException($"RoleActions Not Found (Id:{query.Id}).");
            return new Response<RoleAction>(entity);
        }
    }
}
