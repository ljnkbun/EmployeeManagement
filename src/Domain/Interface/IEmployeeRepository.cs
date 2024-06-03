using Core.Repositories;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IEmployeeRepository : IGenericRepositoryAsync<Employee>
    {
        Task<bool> AddEmployeeAsync(Employee entity, int[] roleIds);
        Task<Employee> GetDeepByIdAsync(int id);
        Task<bool> IsUniqueAsync(string username);
        Task<bool> UpdateEmployeeAsync(Employee entity, int[]? roleIds);
    }
}
