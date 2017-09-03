using Phonebook.Data.Context;
using Phonebook.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Phonebook.Domain.Model;

namespace Phonebook.Data.Repositories
{
    public class ContactRepository : IRepository<Contact>
    {
        private readonly PhonebookContext _phonebookContext;

		public ContactRepository(PhonebookContext phonebookContext)
        {
			_phonebookContext = phonebookContext;
        }

		public IQueryable<Contact> GetAll()
        {
            return _phonebookContext.Contacts;
        }

        public Contact Get(Guid id)
        {
            return _phonebookContext.Contacts.FirstOrDefault(c => c.Id == id);
        }

        public Guid Create(Contact model)
        {
            model.Id = Guid.NewGuid();

            _phonebookContext.Contacts.Add(model);

            return model.Id;
        }

        public void Update(Contact model)
        {
            var contact = Get(model.Id);

            if (contact != null)
            {
                contact.Title = model.Title;
                contact.Forename = model.Forename;
                contact.Surname = model.Surname;
                contact.Email = model.Email;
            }
        }

        public void Delete(Guid id)
        {
			_phonebookContext.ContactNumbers.RemoveRange(_phonebookContext.ContactNumbers.Where(cn => cn.ContactId == id));

            _phonebookContext.Contacts.Remove(_phonebookContext.Contacts.FirstOrDefault(c => c.Id == id));
        }
    }
}
