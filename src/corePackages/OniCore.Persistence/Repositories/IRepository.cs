using Microsoft.EntityFrameworkCore.Query;
using OniCore.Persistence.Pagination;
using System.Linq.Expressions;

namespace OniCore.Persistence.Repositories
{
    // TODO: Add GetListByDynamic method
    public interface IRepository<TEntity> where TEntity : Entity, new()
    {
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IPagedList<TEntity> GetList(int pageIndex = 0, int pageSize = 10,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false);

        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);

        bool Any(Expression<Func<TEntity, bool>>? predicate = null);
    }
}
