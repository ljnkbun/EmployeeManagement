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

        public EmployeeRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _employees = _dbContext.Set<Employee>();
        }

        public async Task<bool> IsUniqueAsync(string username)
        {
            return await _employees.AllAsync(x => x.Username != username);
        }
    }
}
