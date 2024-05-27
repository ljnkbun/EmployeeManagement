using AutoMapper;
using Core.Repositories;
using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class EmployeeRoleRepository : GenericRepositoryAsync<EmployeeRole>, IEmployeeRoleRepository
    {
        private readonly DbSet<EmployeeRole> _employeeRoles;

        public EmployeeRoleRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _employeeRoles = _dbContext.Set<EmployeeRole>();
        }
    }
}
