using AutoMapper;
using Core.Repositories;
using Domain.Entities;
using Domain.Interface;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class DivisionRepository : GenericRepositoryAsync<Division>, IDivisionRepository
    {
        private readonly DbSet<Division> _divisions;

        public DivisionRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _divisions = _dbContext.Set<Division>();
        }

        public async Task<bool> IsUniqueAsync(string code)
        {
            return await _divisions.AllAsync(x => x.Code != code);
        }

    }
}
