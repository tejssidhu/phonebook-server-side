using System;

namespace Phonebook.Domain.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string objectName)
            : base(objectName + " doesn't exists")
        {

        }
    }
}
