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
        private readonly DbSet<RoleAction> _roleActions;
        private readonly DbSet<UserRole> _userRoles;

        public RoleRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _roles = _dbContext.Set<Role>();
            _roleActions = _dbContext.Set<RoleAction>();
            _userRoles = _dbContext.Set<UserRole>();
        }

        public async Task AddRoleAsync(Role entity)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                await _roles.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                //add action logout for user
                var roleActionLogout = new RoleAction()
                {
                    Action = "Logout",
                    Controller = "Auth",
                    RoleId = entity.Id,
                };
                await _roleActions.AddAsync(roleActionLogout);

                await _dbContext.SaveChangesAsync();

                await trans.CommitAsync();
            }
            catch (Exception e)
            {
                await trans.RollbackAsync();
                throw;
            }

        }

        public async Task DeleteRoleAsync(Role entity)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var userRole = _userRoles.Where(x => x.RoleId == entity.Id);
                if (userRole.Any())
                {
                    _userRoles.RemoveRange(userRole);

                    await _dbContext.SaveChangesAsync();
                }
                _roles.Remove(entity);

                await _dbContext.SaveChangesAsync();

                await trans.CommitAsync();
            }
            catch (Exception e)
            {
                await trans.RollbackAsync();
                throw;
            }
        }

        public async Task<ICollection<Role>> GetAllUserRoleByIds(int[]? roleIds)
        {
            if (!roleIds?.Any() == false)
            {
                return new List<Role>();
            }
            return await _roles.Where(x => roleIds!.Contains(x.Id)).ToListAsync();
        }

        public async Task<Role> GetDeepByIdAsync(int id)
        {
            return await _roles.Include(x => x.UserRoles).Include(x => x.RoleActions).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsUniqueAsync(string code)
        {
            return await _roles.AllAsync(x => x.Code != code);
        }

        public async Task UpdateRoleAsync(Role entity)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _roles.Update(entity);
                await _dbContext.SaveChangesAsync();

                //add action logout for user
                var check = await _roleActions.AnyAsync(x => x.Controller == "Auth" && x.Action == "Logout" && x.RoleId == entity.Id);
                if (!check)
                {
                    var roleActionLogout = new RoleAction()
                    {
                        Action = "Logout",
                        Controller = "Auth",
                        RoleId = entity.Id,
                    };
                    await _roleActions.AddAsync(roleActionLogout);

                    await _dbContext.SaveChangesAsync();
                }
                await trans.CommitAsync();
            }
            catch (Exception e)
            {
                await trans.RollbackAsync();
                throw;
            }
        }
    }
}
