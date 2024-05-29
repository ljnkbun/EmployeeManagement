using Core.Repositories;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IAppUserRepository : IGenericRepositoryAsync<AppUser>
    {
        Task<AppUser> GetDeepByIdAsync(int userId);
        Task<bool> IsUniqueAsync(string username);
        Task<AppUser> ValidateUser(string username, string password);
    }
}
