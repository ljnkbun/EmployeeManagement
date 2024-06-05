using Core.Repositories;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IControllerActionRepository : IGenericRepositoryAsync<ControllerAction>
    {
        Task<ICollection<ControllerAction>> GetByIdsAsync(int[]? ids);
    }
}
