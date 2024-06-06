using Core.Repositories;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IDivisionRepository : IGenericRepositoryAsync<Division>
    {
        Task<bool> IsUniqueAsync(string code);
    }
}
