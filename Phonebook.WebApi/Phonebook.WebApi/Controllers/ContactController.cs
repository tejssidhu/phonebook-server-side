using Phonebook.Domain.Interfaces.Services;
using Phonebook.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Phonebook.WebApi.Controllers
{
    public class ContactController : ApiController
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;

        public ContactController(IContactService contactService, IUserService userService)
        {
            _contactService = contactService;
            _userService = userService;
        }

        public IEnumerable<ContactDTO> GetAll()
        {
            var result = new List<ContactDTO>();
            var items = _contactService.GetAll().ToList();
            foreach (var item in items)
            {
                result.Add(new ContactDTO
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    Title = item.Title,
                    Forename = item.Forename,
                    Surname = item.Surname,
                    Email = item.Email
                });
            }

            return result.AsEnumerable();
        }

        public IHttpActionResult GetContact(Guid id)
        {
            var item = _contactService.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(new ContactDTO {
                Id = item.Id,
                UserId = item.UserId,
                Title = item.Title,
                Forename = item.Forename,
                Surname = item.Surname,
                Email = item.Email
            });
        }
    }
}
