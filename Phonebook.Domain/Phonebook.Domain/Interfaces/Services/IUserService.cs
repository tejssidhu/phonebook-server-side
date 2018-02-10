using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;

namespace Phonebook.Domain.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        IEnumerable<Contact> GetContacts(Guid userId);
    }
}
