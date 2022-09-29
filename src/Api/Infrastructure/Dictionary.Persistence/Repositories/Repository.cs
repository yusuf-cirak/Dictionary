using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Domain.Entities.Common;
using Dictionary.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Persistence.Repositories
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : BaseEntity
    where TContext : DbContext
    {
        protected TContext Context { get; }


        public Repository(TContext context)
        {
            Context = context;
        }

        protected DbSet<TEntity> Table => Context.Set<TEntity>();


        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);

            return await Context.SaveChangesAsync();
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);

            return await Context.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            Table.Attach(entity); // Tracks entity if not tracked

            Context.Entry(entity).State = EntityState.Modified;

            return await Context.SaveChangesAsync();
        }

        public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
        {
            // check the entity with id already tracked
            if (!Table.Local.Any(e => EqualityComparer<Guid>.Default.Equals(e.Id, entity.Id)))
            {
                Context.Update(entity);
            }

            return await Context.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            TEntity entity = await Table.FindAsync(id);

            return await DeleteAsync(entity);
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Table.Attach(entity);
            }

            Table.Remove(entity);

            return await Context.SaveChangesAsync();

        }

        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Context.RemoveRange(Table.Where(predicate));
            return await Context.SaveChangesAsync() > 0;
        }

        public virtual IQueryable<TEntity> AsQueryable() => Table.AsQueryable();

        public virtual IQueryable<TEntity> GetAsync(Expression<Func<TEntity, bool>>? predicate, bool asNoTracking = true, params Expression<Func<TEntity, object>>[]? includes)
        {
            var query = AsQueryable();

            if (asNoTracking) query = query.AsNoTracking();

            if (includes != null) query = ApplyIncludes(query, includes);

            if (predicate != null) query = query.Where(predicate);

            return query;

        }

        public virtual async Task<List<TEntity>> GetAll(bool asNoTracking = true)
        {
            IQueryable<TEntity> query = Table;

            if (asNoTracking) query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>>? predicate, bool asNoTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[]? includes)
        {
            var query = AsQueryable();

            if (asNoTracking) query = query.AsNoTracking();

            if (includes != null) query = ApplyIncludes(query, includes);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null) query = orderBy(query);

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool asNoTracking = true, params Expression<Func<TEntity, object>>[]? includes)
        {
            TEntity entity = await Table.FindAsync(id);
            if (entity == null) return null;

            if (asNoTracking) Context.Entry(entity).State = EntityState.Detached;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    await Context.Entry(entity).Reference(include).LoadAsync(); // Lazy loading
                }
            }

            return entity;
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>>? predicate, bool asNoTracking = true, params Expression<Func<TEntity, object>>[]? includes)
        {
            IQueryable<TEntity> query = Table;

            if (asNoTracking) query = query.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            if (includes != null) query = ApplyIncludes(query, includes);

            return await query.SingleOrDefaultAsync();
        }




        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate, bool asNoTracking = true, params Expression<Func<TEntity, object>>[]? includes)
        {
            var query = AsQueryable();

            if (asNoTracking) query=query.AsNoTracking();

            if (includes != null) query = ApplyIncludes(query, includes);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task BulkAddAsync(IEnumerable<TEntity>? entities)
        {
            if (entities == null && !entities.Any()) await Task.CompletedTask;

            await Table.AddRangeAsync(entities);

            await Context.SaveChangesAsync();
        }

        public virtual async Task BulkUpdateAsync(IEnumerable<TEntity> entities)
        {
            if (!entities.Any()) await Task.CompletedTask;

            Table.UpdateRange(entities);

            await Context.SaveChangesAsync();
        }

        public virtual async Task BulkDeleteAsync(IEnumerable<TEntity> entities)
        {
            if (!entities.Any()) await Task.CompletedTask;

            Table.RemoveRange(entities);

            await Context.SaveChangesAsync();
        }

        public virtual async Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Context.RemoveRange(Table.Where(predicate));

            await Context.SaveChangesAsync();
        }

        public virtual Task BulkDeleteByIdsAsync(IEnumerable<Guid>? ids)
        {
            if (ids == null && !ids.Any()) return Task.CompletedTask;

            Context.RemoveRange(Table.Where(e => ids.Contains(e.Id)));

            return Context.SaveChangesAsync();
        }




        private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
