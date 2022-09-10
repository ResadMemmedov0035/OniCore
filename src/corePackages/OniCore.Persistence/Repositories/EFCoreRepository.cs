using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OniCore.CrossCuttingConcerns.Exceptions.CustomExceptions;
using OniCore.Persistence.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Persistence.Repositories
{
    public class EFCoreRepository<TEntity> : IRepository<TEntity>, IAsyncRepository<TEntity>, IQuery<TEntity>
        where TEntity : Entity, new()
    {
        protected DbContext Context { get; }

        public EFCoreRepository(DbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Source => Context.Set<TEntity>();

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Source.FirstOrDefault(predicate)
                ?? throw new NotFoundException("No item exists for this condition.");
        }

        public IPagedList<TEntity> GetList(PaginationParams paginationParams,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false)
        {
            IQueryable<TEntity> query = enableTracking ? Source.AsTracking() : Source.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) query = orderBy(query);
            if (include != null) query = include(query);

            return query.ToPagedList(paginationParams.Index, paginationParams.Size);
        }

        public TEntity Add(TEntity entity)
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


        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Source.FirstOrDefaultAsync(predicate)
                ?? throw new NotFoundException("No item exists for this condition.");
        }

        public async Task<IPagedList<TEntity>> GetListAsync(PaginationParams paginationParams,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = enableTracking ? Source.AsTracking() : Source.AsNoTracking();

            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null) queryable = orderBy(queryable);
            if (include != null) queryable = include(queryable);

            return await queryable.ToPagedListAsync(paginationParams.Index, paginationParams.Size, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
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
