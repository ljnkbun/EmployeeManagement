using Core.Repositories;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IRoleRepository : IGenericRepositoryAsync<Role>
    {
        Task AddRoleAsync(Role entity);
        Task<ICollection<Role>> GetAllUserRoleByIds(int[]? roleIds);
        Task<bool> IsUniqueAsync(string code);
        Task UpdateRoleAsync(Role entity);
    }
}
