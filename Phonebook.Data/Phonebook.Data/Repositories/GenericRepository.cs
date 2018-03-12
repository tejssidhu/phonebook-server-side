using Phonebook.Data.Context;
using Phonebook.Domain.Interfaces.Model;
using Phonebook.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Phonebook.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
	{
		private readonly IPhonebookContext _phonebookContext;
		private DbSet<TEntity> dbSet;

		public GenericRepository(IPhonebookContext phonebookContext)
		{
			_phonebookContext = phonebookContext;
			dbSet = _phonebookContext.Set<TEntity>();
		}

		public virtual IQueryable<TEntity> GetAll(
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
				return orderBy(query);
			}
			else
			{
				return query;
			}
		}

		public virtual IQueryable<TEntity> Get(Guid id)
		{
			return dbSet.Where(e => e.Id == id);
		}

		public virtual void Create(TEntity model)
		{
			dbSet.Add(model);
		}

		public virtual void Update(TEntity model)
		{
			dbSet.Attach(model);
			_phonebookContext.SetModified(model);
		}

		public virtual void Delete(Guid id)
		{
			TEntity entityToDelete = dbSet.Find(id);
			Delete(entityToDelete);
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (_phonebookContext.GetState(entityToDelete) == EntityState.Detached)
			{
				dbSet.Attach(entityToDelete);
			}

			dbSet.Remove(entityToDelete);
		}

		public virtual void DeleteMany(Expression<Func<TEntity, bool>> filter)
		{
			var itemsToDelete = GetAll(filter).ToList();

			foreach (var item in itemsToDelete)
			{
				Delete(item);
			}
		}
	}
}
