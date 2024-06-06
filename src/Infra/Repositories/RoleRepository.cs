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
                //auto add controllerAction get if has role_get
                if (entity.RoleActions.Any(x => x.Controller == "Role" && x.Action == "Get"))
                {
                    var roleGetRole = await _roles.Include(x => x.RoleActions).ThenInclude(x => x.ControllerAction).FirstOrDefaultAsync(x => x.Code == "Role_Get");

                    entity.RoleActions?.Add(new()
                    {
                        Action = "Get",
                        Controller = "ControllerAction",
                        ControllerActionId = roleGetRole.RoleActions.FirstOrDefault(x => x.Controller == "Role" && x.Action == "Get").Id,
                        RoleId = roleGetRole.Id
                    });
                }
                await _roles.AddAsync(entity);
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
                await UpdateRoleAction(entity);
                _roles.Update(entity);

                await _dbContext.SaveChangesAsync();
                await trans.CommitAsync();
            }
            catch (Exception e)
            {
                await trans.RollbackAsync();
                throw;
            }
        }

        private async Task UpdateRoleAction(Role entity)
        {
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
                entity.RoleActions?.Add(roleActionLogout);
            }
            //auto add controllerAction get if has role_get
            if (entity.RoleActions.Any(x => x.Controller == "Role" && x.Action == "Get"))
            {
                var roleGetRole = await _roles.Include(x => x.RoleActions).ThenInclude(x=>x.ControllerAction).FirstOrDefaultAsync(x => x.RoleActions.Any(o=>o.ControllerAction.Code=="Role_Get"));

                entity.RoleActions?.Add(new()
                {
                    Action = "Get",
                    Controller = "ControllerAction",
                    ControllerActionId = roleGetRole.RoleActions.FirstOrDefault(x => x.Controller == "Role" && x.Action == "Get").ControllerAction.Id,
                    RoleId = roleGetRole.Id
                });
            }
            var controllerActionIds = entity.RoleActions?.Select(x => x.ControllerActionId)?.ToList();

            var deleteRoleAction = await _roleActions.Where(x => x.RoleId == entity.Id
                && !controllerActionIds!.Contains(x.ControllerActionId)
            ).ToListAsync();

            if (deleteRoleAction is not null && deleteRoleAction.Any())
            {
                _roleActions.RemoveRange(deleteRoleAction);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
