using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CTEMS.Infrastructure
{
	public interface IUnitOfWork
	{
		IGenericRepository<T> Repository<T>() where T : class;

		int Commit();

		void Rollback();
        Task<int> CommitAsync();

        Task RollbackAsync();
    }
}
