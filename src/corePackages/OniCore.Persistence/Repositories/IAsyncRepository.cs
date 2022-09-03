using Microsoft.EntityFrameworkCore.Query;
using OniCore.Persistence.Pagination;
using System.Linq.Expressions;

namespace OniCore.Persistence.Repositories
{
    // TODO: Add GetListByDynamicAsync method
    public interface IAsyncRepository<TEntity> where TEntity : Entity, new()
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IPagedList<TEntity>> GetListAsync(int pageIndex = 0, int pageSize = 10,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false,
            CancellationToken cancellationToken = default);

        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null);
    }
}
