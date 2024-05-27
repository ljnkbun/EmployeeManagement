using AutoMapper;
using Core.Repositories;
using Domain.Entities;
using Domain.Interface;
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
    }
}
