using Phonebook.Data.Context;
using Phonebook.Domain.Interfaces.Model;
using Phonebook.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
	{
		private readonly PhonebookContext _phonebookContext;
		private DbSet<TEntity> dbSet;

		public GenericRepository(PhonebookContext phonebookContext)
		{
			_phonebookContext = phonebookContext;
			dbSet = _phonebookContext.Set<TEntity>();
		}

		public virtual IEnumerable<TEntity> GetAll(
		  Expression<Func<TEntity, bool>> filter = null,
		  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
		  string includeProperties = "")
		{
			IQueryable<TEntity> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}

		public virtual TEntity Get(Guid id)
		{
			return dbSet.Find(id);
		}

		public virtual void Create(TEntity model)
		{
			dbSet.Add(model);
		}

		public virtual void Update(TEntity model)
		{
			dbSet.Attach(model);
			_phonebookContext.Entry(model).State = EntityState.Modified;
		}

		public virtual void Delete(Guid id)
		{
			TEntity entityToDelete = dbSet.Find(id);
			Delete(entityToDelete);
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (_phonebookContext.Entry(entityToDelete).State == EntityState.Detached)
			{
				dbSet.Attach(entityToDelete);
			}

			dbSet.Remove(entityToDelete);
		}

		public virtual void DeleteMany(Expression<Func<TEntity, bool>> filter)
		{
			var itemsToDelete = GetAll(filter);

			foreach (var item in itemsToDelete)
			{
				Delete(item);
			}
		}
	}
}
