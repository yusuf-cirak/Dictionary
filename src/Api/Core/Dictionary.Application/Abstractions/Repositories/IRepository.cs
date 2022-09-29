using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities.Common;

namespace Dictionary.Application.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        // Write functions
        Task<int> AddAsync(TEntity entity);

        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<int> UpdateAsync(TEntity entity);

        Task<int> AddOrUpdateAsync(TEntity entity);

        Task<int> DeleteAsync(Guid id);

        Task<int> DeleteAsync(TEntity entity);

        Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);

        // Read functions

        IQueryable<TEntity> AsQueryable();

        IQueryable<TEntity> GetAsync(Expression<Func<TEntity, bool>>? predicate, bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<List<TEntity>> GetAll(bool asNoTracking = true);

        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>>? predicate, bool asNoTracking = true,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<TEntity> GetByIdAsync(Guid id, bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>>? predicate, bool asNoTracking = true, params Expression<Func<TEntity, object>>[]? includes);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>>? predicate, bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[]? includes);



        // Bulk write functions
        Task BulkAddAsync(IEnumerable<TEntity> entities);

        Task BulkUpdateAsync(IEnumerable<TEntity> entities);

        Task BulkDeleteAsync(IEnumerable<TEntity> entities);

        Task BulkDeleteAsync(Expression<Func<TEntity, bool>> predicate);

        Task BulkDeleteByIdsAsync(IEnumerable<Guid>? ids);


    }
}
