using Core.Repositories;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IEmployeeRepository : IGenericRepositoryAsync<Employee>
    {
        Task<bool> IsUniqueAsync(string username);
    }
}
