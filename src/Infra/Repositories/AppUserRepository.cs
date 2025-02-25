using AutoMapper;
using Core.Repositories;
using Domain.Entities;
using Domain.Interface;
using Infra.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class AppUserRepository : GenericRepositoryAsync<AppUser>, IAppUserRepository
    {
        private readonly DbSet<AppUser> _appUsers;

        public AppUserRepository(EmployeeManagementDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _appUsers = _dbContext.Set<AppUser>();
        }

        public async Task<AppUser> GetDeepByIdAsync(int userId)
        {
            return await _appUsers.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<bool> IsUniqueAsync(string username)
        {
            return await _appUsers.AllAsync(x => x.Username != username);
        }

        public async Task<AppUser> ValidateUser(string username, string password)
        {
            var passwordHasher= new PasswordHasher<object?>();
            var hashPass = new PasswordHasher<object?>().HashPassword(null!, password);
            return await _appUsers.FirstOrDefaultAsync(x => x.Username == username && passwordHasher.VerifyHashedPassword(null!, hashPass, password) == PasswordVerificationResult.Success) ?? null!;
        }
    }
}
