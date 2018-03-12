using Phonebook.Domain.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Domain.Interfaces.Services
{
	public interface IService<TEntity> : IDisposable where TEntity : IEntity
	{
		IQueryable<TEntity> GetAll();
		IQueryable<TEntity> Get(Guid id);
		Guid Create(TEntity model);
		void Update(TEntity model);
		void Delete(Guid id);
	}
}
