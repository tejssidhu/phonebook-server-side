using System;

namespace Phonebook.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("Invalid password")
        {
            
        }
    }
}
