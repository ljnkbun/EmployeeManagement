using Core.Repositories;
using Domain.Entities;

namespace Domain.Interface
{
    public interface IEmployeeRepository : IGenericRepositoryAsync<Employee>
    {
        Task<bool> AddEmployeeAsync(Employee entity, int[] roleIds);
        Task DeleteEmployeeAsync(Employee entity);
        Task<Employee> GetDeepByIdAsync(int id);
        Task<bool> IsUniqueAsync(string username);
        Task<bool> IsUniqueCodeAsync(string code);
        Task<bool> UpdateEmployeeAsync(Employee entity, int[]? roleIds);
    }
}
