using KolodvorApp.Domain;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Persistance.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace KolodvorApp.Persistance;

public class Repository<TEntity> : IRepository<TEntity>, IDisposable
    where TEntity : class, IEntity
{
    internal readonly KolodvorAppContext _dbContext;

    public Repository(KolodvorAppContext context)
    {
        _dbContext = context;
    }

    public async Task<TEntity> GetAsync(Guid id)
    {
        return await FindAsync(entity => entity.Id == id) ?? throw new KeyNotFoundException();
    }

    public async Task<TEntity?> FindAsync([NotNull] Expression<Func<TEntity, bool>> predicate)
    {
        return await GetDbSet().Where(predicate).FirstOrDefaultAsync();
    }

    public virtual IQueryable<TEntity> GetAll() => GetDbSet().AsQueryable();

    public IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
    {
        return Repository<TEntity>.IncludeDetails(
            GetDbSet().AsQueryable(),
            propertySelectors);
    }

    public IEnumerable<object> WithDeepDetails(params Expression<Func<object, object>>[] propertySelectors)
    {
        return Repository<TEntity>.IncludeDetails(
            GetDbSet().AsQueryable(),
            propertySelectors[0],
            propertySelectors[1]);
    }

    public async Task<TEntity> InsertAsync([NotNull] TEntity entity)
    {
        var savedEntity = (await GetDbSet().AddAsync(entity)).Entity;

        await _dbContext.SaveChangesAsync();

        return savedEntity;
    }

    public async Task<TEntity> UpdateAsync([NotNull] TEntity entity)
    {
        var updatedEntity = _dbContext.Update(entity).Entity;

        await _dbContext.SaveChangesAsync();

        return updatedEntity;
    }

    public virtual async Task DeleteAsync(Guid key)
    {
        var entity = await FindAsync(entity => entity.Id == key) ?? throw new KeyNotFoundException();

        GetDbSet().Remove(entity);

        await _dbContext.SaveChangesAsync();
    }

    private DbSet<TEntity> GetDbSet()
    {
        return _dbContext.Set<TEntity>();
    }

    private static IQueryable<TEntity> IncludeDetails(
        IQueryable<TEntity> query,
        Expression<Func<TEntity, object>>[] propertySelectors)
    {
        if (propertySelectors.Length > 0)
        {
            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }
        }

        return query;
    }

    private static IEnumerable<object> IncludeDetails(
        IQueryable<TEntity> query,
        Expression<Func<object, object>> rootPropertySelector,
        Expression<Func<object, object>> detailPropertySelector)
    {
        var includableQuery = query.Include(rootPropertySelector).ThenInclude(detailPropertySelector).ToList();

        return includableQuery;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}