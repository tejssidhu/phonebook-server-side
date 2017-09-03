using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phonebook.WebApi.Models
{
    public class ContactDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}