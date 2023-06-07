using LostAndFound.Database.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace LostAndFound.Database.Base
{
    public class RepoUnitOfWork : IDisposable
    {
        private LostAndFoundContext _context;
        private IDbContextTransaction _transaction;

        public RepoUnitOfWork(bool beginTransaction = false)
        {
            _context = new LostAndFoundContext();
            if (beginTransaction)
            {
                _transaction = _context.Database.BeginTransaction();
            }
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
            {
                return repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            IRepository<TEntity> repository = new GenericRepository<TEntity>(_context);
            repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public int SaveChanges()
        {
            var result = _context.SaveChanges();
            return result;
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

        public void CommitTransaction()
        {
            _transaction?.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
        }
    }
}