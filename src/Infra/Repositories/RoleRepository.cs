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

        public RoleRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _roles = _dbContext.Set<Role>();
            _roleActions = _dbContext.Set<RoleAction>();
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

        public async Task<ICollection<Role>> GetAllUserRoleByIds(int[]? roleIds)
        {
            if (!roleIds?.Any() == false)
            {
                return new List<Role>();
            }
            return await _roles.Where(x => roleIds!.Contains(x.Id)).ToListAsync();
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
