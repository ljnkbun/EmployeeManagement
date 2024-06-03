using AutoMapper;
using Core.Repositories;
using Domain.Entities;
using Domain.Interface;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class EmployeeRepository : GenericRepositoryAsync<Employee>, IEmployeeRepository
    {
        private readonly DbSet<Employee> _employees;
        private readonly DbSet<UserRole> _userRoles;

        public EmployeeRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _employees = _dbContext.Set<Employee>();
            _userRoles = _dbContext.Set<UserRole>();
        }

        public async Task<Employee> GetDeepByIdAsync(int id)
        {
            return await _employees.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> AddEmployeeAsync(Employee entity, int[]? roleIds)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                await _employees.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                if (roleIds?.Any() == false)
                {
                    
                }
                else
                {
                    var userRolesAdd = new List<UserRole>();
                    foreach (var roleId in roleIds!)
                    {
                        userRolesAdd.Add(new()
                        {
                            AppUserId = entity.Id,
                            RoleId = roleId
                        });
                    }
                    await _userRoles.AddRangeAsync(userRolesAdd);

                    await _dbContext.SaveChangesAsync();
                }

                await trans.CommitAsync();
            }
            catch (Exception e)
            {
                await trans.RollbackAsync();
                throw;
            }
            return true;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee entity, int[]? roleIds)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (roleIds?.Any() == false)
                {
                    _employees.Update(entity);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    var userRolesOld = _userRoles.Where(x => x.AppUserId == entity.Id);
                    _userRoles.RemoveRange(userRolesOld);

                    var userRolesAdd = new List<UserRole>();
                    foreach (var roleId in roleIds!)
                    {
                        userRolesAdd.Add(new()
                        {
                            AppUserId = entity.Id,
                            RoleId = roleId
                        });
                    }
                    await _userRoles.AddRangeAsync(userRolesAdd);

                    await _dbContext.SaveChangesAsync();
                }

                await trans.CommitAsync();
            }
            catch (Exception e)
            {
                await trans.RollbackAsync();
                throw;
            }
            return true;
        }


        public async Task<bool> IsUniqueAsync(string username)
        {
            return await _employees.AllAsync(x => x.Username != username);
        }
    }
}
