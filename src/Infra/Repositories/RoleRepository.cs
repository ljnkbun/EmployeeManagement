using AutoMapper;
using Core.Repositories;
using Domain.Entities;
using Domain.Interface;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class RoleRepository : GenericRepositoryAsync<Role>, IRoleRepository
    {
        private readonly DbSet<Role> _roles;

        public RoleRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _roles = _dbContext.Set<Role>();
        }

        public async Task<bool> IsUniqueAsync(string code)
        {
            return await _roles.AllAsync(x => x.Code != code);
        }
    }
}
