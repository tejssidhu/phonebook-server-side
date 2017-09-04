using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;

namespace Phonebook.WebApi.Controllers
{
    public class ContactsController : ODataController
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IHttpActionResult Get()
        {
            var result = new List<Contact>();
            var items = _contactService.GetAll().ToList();
            
            return Ok(items);
        }

        public IHttpActionResult Get([FromODataUri]Guid key)
        {
            var item = _contactService.Get(key);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        public IHttpActionResult Post(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Guid contactId = _contactService.Create(contact);

            return Created(contact);
        }

        public IHttpActionResult Put([FromODataUri] Guid key, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != contact.Id)
            {
                return BadRequest();
            }

            try
            {
                _contactService.Update(contact);
            }
            catch
            {
                throw;
            }

            return Ok(contact);
        }

        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            var contact = _contactService.Get(key);
            if (contact == null)
            {
                return NotFound();
            }
            _contactService.Delete(key);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpGet]
        [ODataRoute("GetByUser(UserId={UserId})")]
        public IHttpActionResult GetByUser([FromODataUri]Guid UserId)
        {
            var result = new List<Contact>();
            var items = _contactService.GetAllByUserId(UserId);

            return Ok(items);
        }
    }
}
