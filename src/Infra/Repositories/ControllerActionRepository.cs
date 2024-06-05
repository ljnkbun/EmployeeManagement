using AutoMapper;
using Core.Repositories;
using Domain.Entities;
using Domain.Interface;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace Infra.Repositories
{
    public class ControllerActionRepository : GenericRepositoryAsync<ControllerAction>, IControllerActionRepository
    {
        private readonly DbSet<ControllerAction> _controllerActions;

        public ControllerActionRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _controllerActions = _dbContext.Set<ControllerAction>();
        }

        public async Task<ICollection<ControllerAction>> GetByIdsAsync(int[]? ids)
        {
            return await _controllerActions.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
