using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Extensions.Objects;
using Core.Models.Requests;
using Core.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly IMapper _mapper;

        public GenericRepositoryAsync(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetReponseAsync<TParam>(TParam parameter)
        {
            return await _dbContext.Set<T>()
                .Filter(parameter)
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<PagedResponse<IReadOnlyList<T>>> GetPagedReponseAsync<TParam>(TParam parameter) where TParam : RequestParameter
        {
            var response = new PagedResponse<IReadOnlyList<T>>(parameter.PageNumber, parameter.PageSize);
            var query = _dbContext.Set<T>().Filter(parameter);
            response.TotalCount = await query.CountAsync();
            response.Data = await query.AsNoTracking()
                .OrderBy(parameter.OrderBy)
                .SearchTerm(parameter.SearchTerm, parameter.GetSearchProps())
                .Paged(parameter.PageSize, parameter.PageNumber)
                .ToListAsync();
            return response;
        }

        public virtual async Task<PagedResponse<IReadOnlyList<TModel>>> GetModelPagedReponseAsync<TParam, TModel>(TParam parameter)
            where TModel : class
            where TParam : RequestParameter
        {
            var response = new PagedResponse<IReadOnlyList<TModel>>(parameter.PageNumber, parameter.PageSize);
            var query = _dbContext.Set<T>().Filter(parameter);
            response.TotalCount = await query.CountAsync();
            response.Data = await query.AsNoTracking()
                    .OrderBy(parameter.OrderBy)
                    .SearchTerm(parameter.SearchTerm, parameter.GetSearchProps())
                    .Paged(parameter.PageSize, parameter.PageNumber)
                    .ProjectTo<TModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            return response;
        }

        public virtual async Task<PagedResponse<IReadOnlyList<TModel>>> GetModelSingleQueryPagedReponseAsync<TParam, TModel>(TParam parameter)
            where TModel : class
            where TParam : RequestParameter
        {
            var response = new PagedResponse<IReadOnlyList<TModel>>(parameter.PageNumber, parameter.PageSize);
            var query = _dbContext.Set<T>().Filter(parameter);
            response.TotalCount = await query.CountAsync();
            response.Data = await query.AsNoTracking()
                    .OrderBy(parameter.OrderBy)
                    .SearchTerm(parameter.SearchTerm, parameter.GetSearchProps())
                    .Paged(parameter.PageSize, parameter.PageNumber)
                    .ProjectTo<TModel>(_mapper.ConfigurationProvider)
                    .AsSingleQuery()
                    .ToListAsync();
            return response;
        }

        public virtual async Task UpdateRangeAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<bool> AddRangeAsync(List<T> entities)
        {
            bool result = true;
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.Set<T>().AddRangeAsync(entities);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    result = false;
                    await transaction.RollbackAsync();
                }
            }

            return result;
        }
    }
}
