using Core.Repositories;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IRoleActionRepository : IGenericRepositoryAsync<RoleAction>
    {
        Task<ICollection<RoleAction>> GetAllByUser(int userId);
    }
}
