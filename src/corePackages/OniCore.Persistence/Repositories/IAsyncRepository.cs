using Microsoft.EntityFrameworkCore.Query;
using OniCore.Persistence.Dynamic;
using OniCore.Persistence.Pagination;
using System.Linq.Expressions;

namespace OniCore.Persistence.Repositories
{
    public interface IAsyncRepository<TEntity> where TEntity : Entity, new()
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false);

        Task<IPagedList<TEntity>> GetListAsync(PageParams pageParams,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false,
            CancellationToken cancellationToken = default);

        Task<IPagedList<TEntity>> GetListAsync(PageParams pageParams,
            DynamicParams dynamicParams,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false,
            CancellationToken cancellationToken = default);

        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null);
    }
}
