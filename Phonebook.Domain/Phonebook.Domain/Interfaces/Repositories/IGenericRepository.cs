using Phonebook.Domain.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Interfaces.Repositories
{
	public interface IGenericRepository<TEntity> where TEntity : class, IEntity
	{
		IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
		  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
		  string includeProperties = "");
		
		TEntity Get(Guid id);
		void Create(TEntity model);
		void Update(TEntity model);
		void DeleteMany(Expression<Func<TEntity, bool>> filter);
		void Delete(Guid id);
		void Delete(TEntity entityToDelete);
	}
}
