using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OniCore.CrossCuttingConcerns.ExceptionHandling.Exceptions;
using OniCore.Persistence.Dynamic;
using OniCore.Persistence.Pagination;
using System.Linq.Expressions;

namespace OniCore.Persistence.Repositories
{
    // TODO:
    // In future, the repository may not support AsTracking. Only AsNoTracking
    // maybe SaveChanges,
    // why IQuery is necessary anyway?

    public class EFCoreRepository<TEntity> : IRepository<TEntity>, IAsyncRepository<TEntity>, IQuery<TEntity>
        where TEntity : Entity, new()
    {
        protected DbContext Context { get; }

        public EFCoreRepository(DbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Source => Context.Set<TEntity>();

        public IQueryable<TEntity> TrackingSource(bool enableTracking) => enableTracking ? Source.AsTracking() : Source.AsNoTracking();


        public TEntity Get(Expression<Func<TEntity, bool>> predicate, 
                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                           bool enableTracking = false)
        {
            var query = TrackingSource(enableTracking);

            TEntity? item = include == null
                          ? query.FirstOrDefault(predicate)
                          : include(query).FirstOrDefault(predicate);

            return item ?? throw new NotFoundException();
        }

        public IPagedList<TEntity> GetList(PageParams pageParams,
                                           Expression<Func<TEntity, bool>>? predicate = null,
                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                           bool enableTracking = false)
        {
            IQueryable<TEntity> query = TrackingSource(enableTracking);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) query = orderBy(query);
            if (include != null) query = include(query);
            return query.ToPagedList(pageParams);
        }

        public IPagedList<TEntity> GetList(PageParams pageParams,
                                           DynamicParams dynamicParams,
                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                           bool enableTracking = false)
        {
            IQueryable<TEntity> query = TrackingSource(enableTracking);
            if (dynamicParams.Filter != null) query = query.Filter(dynamicParams.Filter);
            if (dynamicParams.Sorts != null) query = query.Sort(dynamicParams.Sorts);
            if (include != null) query = include(query);
            return query.ToPagedList(pageParams);
        }

        public TEntity Create(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges();
            return entity;
        }

        public bool Any(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null ? Source.Any() : Source.Any(predicate);
        }


        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                            bool enableTracking = false)
        {
            var query = TrackingSource(enableTracking);

            TEntity? item = include == null
                          ? await query.FirstOrDefaultAsync(predicate)
                          : await include(query).FirstOrDefaultAsync(predicate);

            return item ?? throw new NotFoundException();
        }

        public async Task<IPagedList<TEntity>> GetListAsync(PageParams pageParams,
                                                            Expression<Func<TEntity, bool>>? predicate = null,
                                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                            bool enableTracking = false,
                                                            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = TrackingSource(enableTracking);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) query = orderBy(query);
            if (include != null) query = include(query);
            return await Task.FromResult(query.ToPagedList(pageParams)).ConfigureAwait(false);
        }

        public async Task<IPagedList<TEntity>> GetListAsync(PageParams pageParams,
                                                            DynamicParams dynamicParams,
                                                            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                                            bool enableTracking = false,
                                                            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = TrackingSource(enableTracking);
            if (dynamicParams.Filter != null) query = query.Filter(dynamicParams.Filter);
            if (dynamicParams.Sorts != null) query = query.Sort(dynamicParams.Sorts);
            if (include != null) query = include(query);
            return await Task.FromResult(query.ToPagedList(pageParams)).ConfigureAwait(false);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null
                 ? await Source.AnyAsync().ConfigureAwait(false)
                 : await Source.AnyAsync(predicate).ConfigureAwait(false);
        }
    }
}
