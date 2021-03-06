﻿using Phonebook.Data.Context;
using Phonebook.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Phonebook.Domain.Model;
using Phonebook.Domain.Exceptions;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Phonebook.Data.Repositories
{
	public class ContactNumberRepository : IContactNumberRepository
	{
		private readonly IPhonebookContext _phonebookContext;
		private DbSet<ContactNumber> dbSet;

		public ContactNumberRepository(IPhonebookContext phonebookContext)
		{
			_phonebookContext = phonebookContext;
			dbSet = _phonebookContext.Set<ContactNumber>();
		}

		public IQueryable<ContactNumber> GetAll(
		  Expression<Func<ContactNumber, bool>> filter = null,
		  Func<IQueryable<ContactNumber>, IOrderedQueryable<ContactNumber>> orderBy = null,
		  string includeProperties = "")
		{
			IQueryable<ContactNumber> query = dbSet;

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

		public IQueryable<ContactNumber> Get(Guid id)
		{
			return dbSet.Where(e => e.Id == id);
		}

		public void Create(ContactNumber model)
		{
			dbSet.Add(model);
		}

		public void Update(ContactNumber model)
		{
			dbSet.Attach(model);
			_phonebookContext.SetModified(model);
		}

		public void Delete(Guid id)
		{
			ContactNumber entityToDelete = dbSet.Find(id);

			if (entityToDelete == null)
				throw new ObjectNotFoundException("Contact");

			Delete(entityToDelete);
		}

		public void Delete(ContactNumber model)
		{
			if (_phonebookContext.GetState(model) == EntityState.Detached)
			{
				dbSet.Attach(model);
			}
			dbSet.Remove(model);
		}

		public void DeleteContactNumbersByContactId(Guid contactId)
		{
			var contactNumbers = _phonebookContext.ContactNumbers.Where(cn => cn.ContactId == contactId).ToList();

			foreach (var contactNumber in contactNumbers)
			{
				Delete(contactNumber);
			}
		}

		public virtual void DeleteMany(Expression<Func<ContactNumber, bool>> filter)
		{
			var itemsToDelete = GetAll(filter);

			foreach (var item in itemsToDelete)
			{
				Delete(item);
			}
		}
	}
}
