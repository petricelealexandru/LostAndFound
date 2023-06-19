using System.Linq.Expressions;

namespace LostAndFound.Database.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>>? expression = null);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
        TEntity GetSingle(Expression<Func<TEntity, bool>> expression, string[] navigationProperties = null);
        IQueryable<TEntity> OrderBy(Expression<Func<TEntity, object>> expression);
        IQueryable<TEntity> OrderByDescending(Expression<Func<TEntity, object>> expression);
        IEnumerable<TEntity> GetAll();
    }
}