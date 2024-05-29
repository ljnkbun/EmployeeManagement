using AutoMapper;
using Core.Repositories;
using Domain.Entities;
using Domain.Interface;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class UserRoleRepository : GenericRepositoryAsync<UserRole>, IUserRoleRepository
    {
        private readonly DbSet<UserRole> _userRoles;

        public UserRoleRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _userRoles = _dbContext.Set<UserRole>();
        }
    }
}
