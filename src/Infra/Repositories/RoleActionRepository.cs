using AutoMapper;
using Core.Repositories;
using Domain.Entities;
using Domain.Interface;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class RoleActionRepository : GenericRepositoryAsync<RoleAction>, IRoleActionRepository
    {
        private readonly DbSet<RoleAction> _roleActions;
        private readonly DbSet<AppUser> _appUsers;
        private readonly DbSet<Role> _roles;

        public RoleActionRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _roleActions = _dbContext.Set<RoleAction>();
            _roles = _dbContext.Set<Role>();
            _appUsers = _dbContext.Set<AppUser>();
        }

        public async Task<ICollection<RoleAction>> GetAllByUser(int userId)
        {
            var user = await _appUsers.Include(x => x.UserRoles).Where(x => x.Id == userId).FirstOrDefaultAsync();
            var userRoleIds = user!.UserRoles?.Select(ur => ur.RoleId) ?? [];
            var roles = await _roles.Where(x => userRoleIds.Contains(x.Id)).ToListAsync();
            var roleIds = roles.Select(r => r.Id) ?? [];

            return await _roleActions.Where(x => roleIds.Contains(x.Role.Id)).ToListAsync();
        }
    }
}
