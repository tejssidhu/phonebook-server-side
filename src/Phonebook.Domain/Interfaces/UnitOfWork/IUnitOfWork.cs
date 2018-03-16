using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Interfaces.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<User> UserRepository { get; }
		IGenericRepository<Contact> ContactRepository { get; }
		IContactNumberRepository ContactNumberRepository { get; }
		void SaveChanges();
	}
}
