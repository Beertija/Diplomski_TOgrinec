using KolodvorApp.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace KolodvorApp.Domain;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<TEntity> GetAsync(Guid id);
    Task<TEntity?> FindAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors);
    Task<TEntity> InsertAsync([NotNull] TEntity entity);
    Task<TEntity> UpdateAsync([NotNull] TEntity entity);
    Task DeleteAsync(Guid key);
}