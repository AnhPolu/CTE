using CTEMS.Lib.Context;
using CTEMS.Lib.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CTEMS.Infrastructure
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly ICurrentUserService _currentUserService;

		public GenericRepository(ApplicationDbContext context, ICurrentUserService currentUserService)
		{
			_context = context;
			_currentUserService = currentUserService;
		}

		public IQueryable<T> GetAll()
		{
			return _context.Set<T>();
		}

		public T GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public T GetSingleBy(Expression<Func<T, bool>> match)
		{
			return _context.Set<T>().SingleOrDefault(match);
		}

		public async Task<T> GetSingleByAsync(Expression<Func<T, bool>> match)
		{
			return await _context.Set<T>().SingleOrDefaultAsync(match);
		}

		public IQueryable<T> GetManyBy(Expression<Func<T, bool>> match)
		{
			return _context.Set<T>().Where(match);
		}

		public T Add(T entity)
		{
			if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
			{
				((IAuditEntity)entity).CreatedDate = DateTime.Now;
				((IAuditEntity)entity).CreatedBy = _currentUserService.CurrentUserId;
				((IAuditEntity)entity).ModifiedDate = null;
				((IAuditEntity)entity).ModifiedBy = null;
			}
			_context.Set<T>().Add(entity);
			return entity;
		}

		public async Task<T> AddAsync(T entity)
		{
			if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
			{
				((IAuditEntity)entity).CreatedDate = DateTime.Now;
				((IAuditEntity)entity).CreatedBy = _currentUserService.CurrentUserId;
				((IAuditEntity)entity).ModifiedDate = null;
				((IAuditEntity)entity).ModifiedBy = null;
			}
			await _context.Set<T>().AddAsync(entity);
			return entity;
		}

		public T Update(T entity)
		{
			if (entity == null)
			{
				return null;
			}

			if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
			{
				((IAuditEntity)entity).ModifiedDate = DateTime.Now;
				((IAuditEntity)entity).ModifiedBy = _currentUserService.CurrentUserId;
			}

			_context.Set<T>().Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;

			return entity;
		}

		public void Delete(T t)
		{
			_context.Set<T>().Remove(t);
		}

		public int Count()
		{
			return _context.Set<T>().Count();
		}

		public async Task<int> CountAsync()
		{
			return await _context.Set<T>().CountAsync();
		}

		public IQueryable<T> Filter(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null,
			int? pageSize = null)
		{
			IQueryable<T> query = _context.Set<T>();
			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			if (includeProperties != null)
			{
				foreach (
					var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			if (page != null && pageSize != null)
			{
				query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
			}

			return query;
		}

		public bool Exist(Expression<Func<T, bool>> predicate)
		{
			var exist = _context.Set<T>().Where(predicate);
			return exist.Any() ? true : false;
		}

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }
    }
}
