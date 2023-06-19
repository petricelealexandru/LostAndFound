using LostAndFound.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LostAndFound.Database.Base
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private LostAndFoundContext _context;
        public GenericRepository(LostAndFoundContext context)
        {
            _context = context;
        }
        public LostAndFoundContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public TEntity Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            var result = _context.SaveChanges();
            if (result == 0)
            {
                return null;
            }

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            var result = _context.SaveChanges();

            if (result == 0)
            {
                return null;
            }

            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            var result = _context.SaveChanges();

            if (result == 0)
            {
                return null;
            }

            return entity;

        }

        //aici nu inteleg
        public TEntity GetSingle(Expression<Func<TEntity, bool>> expression, string[] navigationProperties = null)
        {
            var query = _context.Set<TEntity>().Where(expression);

            if (navigationProperties != null)
            {
                foreach (var includeProperty in navigationProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>>? expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }

        public IQueryable<TEntity> OrderBy(Expression<Func<TEntity, object>> expression)
        {
            return _context.Set<TEntity>().OrderBy(expression);
        }

        public IQueryable<TEntity> OrderByDescending(Expression<Func<TEntity, object>> expression)
        {
            return _context.Set<TEntity>().OrderByDescending(expression);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
