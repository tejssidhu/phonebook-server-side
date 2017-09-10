using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;

namespace Phonebook.WebApi.Controllers
{
    public class ContactNumbersController : ODataController
    {
        private readonly IContactNumberService _service;

        public ContactNumbersController(IContactNumberService service)
        {
            _service = service;
        }

        public IHttpActionResult Get()
        {
            var result = new List<ContactNumber>();
            var items = _service.GetAll().ToList();

            return Ok(items);
        }

        public IHttpActionResult Get([FromODataUri]Guid key)
        {
            var item = _service.Get(key);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        public IHttpActionResult Post(ContactNumber entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Guid contactNumberId = _service.Create(entity);
            entity.Id = contactNumberId;

            return Created(entity);
        }

        public IHttpActionResult Put([FromODataUri] Guid key, ContactNumber ContactNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != ContactNumber.Id)
            {
                return BadRequest();
            }

            try
            {
                _service.Update(ContactNumber);
            }
            catch
            {
                throw;
            }

            return Ok(ContactNumber);
        }

        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            var ContactNumber = _service.Get(key);
            if (ContactNumber == null)
            {
                return NotFound();
            }
            _service.Delete(key);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}
