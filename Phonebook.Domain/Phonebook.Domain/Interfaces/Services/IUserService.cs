using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;

namespace Phonebook.Domain.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        User Authenticate(string username, string password);
    }
}
