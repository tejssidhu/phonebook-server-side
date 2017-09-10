using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;

namespace Phonebook.Domain.Interfaces.Services
{
    public interface IContactService : IService<Contact>
	{
		IEnumerable<Contact> Search(Guid userId, string name, string email);
		IEnumerable<Contact> SearchContactsByName(Guid userId, string forename, string surname);
		IEnumerable<Contact> SearchContactsByEmail(Guid userId, string email);
        IEnumerable<ContactNumber> GetContactNumbers(Guid contactId);
    }
}
