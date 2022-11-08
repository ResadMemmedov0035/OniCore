using Microsoft.EntityFrameworkCore.Query;
using OniCore.Persistence.Dynamic;
using OniCore.Persistence.Pagination;
using System.Linq.Expressions;

namespace OniCore.Persistence.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity, new()
    {
        TEntity Get(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false);

        IPagedList<TEntity> GetList(PageParams pageParams,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false);

        IPagedList<TEntity> GetList(PageParams pageParams,
            DynamicParams dynamicParams,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false);

        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);

        bool Any(Expression<Func<TEntity, bool>>? predicate = null);
    }
}
