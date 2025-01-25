using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CTEMS.Infrastructure
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        IQueryable<T> GetAll();

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        T GetSingleBy(Expression<Func<T, bool>> match);

        Task<T> GetSingleByAsync(Expression<Func<T, bool>> match);
        IQueryable<T> GetManyBy(Expression<Func<T, bool>> match);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        Task<T> Update(T updated);

        Task Delete(T t);

        int Count();

        Task<int> CountAsync();

        IQueryable<T> Filter(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        bool Exist(Expression<Func<T, bool>> predicate);
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
    }
}
