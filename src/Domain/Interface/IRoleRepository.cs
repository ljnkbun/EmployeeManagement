using Core.Repositories;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IRoleRepository : IGenericRepositoryAsync<Role>
    {
        Task<bool> IsUniqueAsync(string code);
    }
}
