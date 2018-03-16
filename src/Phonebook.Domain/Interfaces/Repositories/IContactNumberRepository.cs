using Phonebook.Domain.Model;
using System;

namespace Phonebook.Domain.Interfaces.Repositories
{
    public interface IContactNumberRepository : IGenericRepository<ContactNumber>
    {
        void DeleteContactNumbersByContactId(Guid contactId);
    }
}
