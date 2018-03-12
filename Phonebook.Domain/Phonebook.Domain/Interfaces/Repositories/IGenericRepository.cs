using Phonebook.Domain.Interfaces.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Phonebook.Domain.Interfaces.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class, IEntity
	{
		IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
		  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
		  string includeProperties = "");

		IQueryable<TEntity> Get(Guid id);
		void Create(TEntity model);
		void Update(TEntity model);
		void DeleteMany(Expression<Func<TEntity, bool>> filter);
		void Delete(Guid id);
		void Delete(TEntity entityToDelete);
	}
}
