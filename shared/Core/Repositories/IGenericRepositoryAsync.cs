using Core.Models.Requests;
using Core.Models.Response;

namespace Core.Repositories
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetReponseAsync<TParam>(TParam parameter);
        Task<PagedResponse<IReadOnlyList<T>>> GetPagedReponseAsync<TParam>(TParam parameter) where TParam : RequestParameter;
        Task<PagedResponse<IReadOnlyList<TModel>>> GetModelPagedReponseAsync<TParam, TModel>(TParam parameter)
            where TModel : class
            where TParam : RequestParameter;
        Task<PagedResponse<IReadOnlyList<TModel>>> GetModelSingleQueryPagedReponseAsync<TParam, TModel>(TParam parameter)
            where TModel : class
            where TParam : RequestParameter;
        Task<T> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entities);
    }
}
