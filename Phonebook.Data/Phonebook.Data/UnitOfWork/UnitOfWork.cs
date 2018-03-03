using Phonebook.Data.Context;
using Phonebook.Data.Repositories;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.UnitOfWork;
using Phonebook.Domain.Model;
using System;

namespace Phonebook.Data
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly IPhonebookContext _phonebookContext;

        // TODO: use dependency injection instead of manually creating instances
		public UnitOfWork(IPhonebookContext phonebookContext)
		{
			_phonebookContext = phonebookContext;

			UserRepository = new GenericRepository<User>(_phonebookContext);
			ContactRepository = new GenericRepository<Contact>(_phonebookContext);
			ContactNumberRepository = new ContactNumberRepository(_phonebookContext);
		}

		public IGenericRepository<User> UserRepository { get; private set; }
		public IGenericRepository<Contact> ContactRepository { get; private set; }
		public IContactNumberRepository ContactNumberRepository { get; private set; }

		public void SaveChanges()
		{
			_phonebookContext.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_phonebookContext.Dispose();
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
