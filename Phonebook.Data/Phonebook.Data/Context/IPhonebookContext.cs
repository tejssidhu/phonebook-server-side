using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Phonebook.Domain.Model;

namespace Phonebook.Data.Context
{
	public interface IPhonebookContext: IDisposable
	{
		DbSet<SampleAddress> SampleAddress { get; set; }
		DbSet<SampleContact> SampleContacts { get; set; }
		DbSet<SampleUser> SampleUsers { get; set; }
		DbSet<ContactNumber> ContactNumbers { get; set; }
		DbSet<Contact> Contacts { get; set; }
		DbSet<User> Users { get; set; }

		DbEntityEntry Entry(object entity);
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		void SetModified(object entity);
		EntityState GetState(object entity);
		void SaveChanges();
	}
}