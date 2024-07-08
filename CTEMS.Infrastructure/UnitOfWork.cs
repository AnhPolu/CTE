using CTEMS.Lib.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTEMS.Infrastructure
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
		private readonly ICurrentUserService _currentUserService;

		public Dictionary<Type, object> Repositories
		{
			get { return _repositories; }
			set { Repositories = value; }
		}

		public UnitOfWork(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
		{
			_dbContext = dbContext;
			_currentUserService = currentUserService;
		}

		public IGenericRepository<T> Repository<T>() where T : class
		{
			if (Repositories.Keys.Contains(typeof(T)))
			{
				return Repositories[typeof(T)] as IGenericRepository<T>;
			}

			IGenericRepository<T> repo = new GenericRepository<T>(_dbContext, _currentUserService);
			Repositories.Add(typeof(T), repo);
			return repo;
		}

		public int Commit()
		{
			return _dbContext.SaveChanges();
		}

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
		{
			_dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
		}

        public async Task RollbackAsync()
        {
			var entries = _dbContext.ChangeTracker.Entries().ToList();
			foreach (var entry in entries)
            {
				await entry.ReloadAsync();
            }
        }


        private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_dbContext.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
