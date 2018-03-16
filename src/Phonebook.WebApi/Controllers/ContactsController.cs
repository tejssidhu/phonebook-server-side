using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using Phonebook.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;

namespace Phonebook.WebApi.Controllers
{
	[Authorize]
	public class ContactsController : ODataController
	{
		private readonly IContactService _service;

		public ContactsController(IContactService contactService)
		{
			_service = contactService;
		}

		[ScopeAuthorise("phonebookAPI.read")]
		public IHttpActionResult Get()
		{
			var result = new List<Contact>();
			var items = _service.GetAll().ToList();

			return Ok(items);
		}

		[ScopeAuthorise("phonebookAPI.read")]
		public IHttpActionResult Get([FromODataUri]Guid key)
		{
			var item = _service.Get(key);
			if (item == null)
			{
				return NotFound();
			}

			return Ok(item);
		}

		[ScopeAuthorise("phonebookAPI.write")]
		public IHttpActionResult Post(Contact contact)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			contact.Id = Guid.NewGuid();
			_service.Create(contact);

			return Created(contact);
		}

		[ScopeAuthorise("phonebookAPI.write")]
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
				_service.Update(contact);
			}
			catch
			{
				throw;
			}

			return Ok(contact);
		}

		[ScopeAuthorise("phonebookAPI.write")]
		public IHttpActionResult Delete([FromODataUri] Guid key)
		{
			var contact = _service.Get(key);
			if (contact == null)
			{
				return NotFound();
			}
			_service.Delete(key);
			return StatusCode(System.Net.HttpStatusCode.NoContent);
		}

		[ScopeAuthorise("phonebookAPI.read")]
		[HttpGet]
		[ODataRoute("Contacts({key})/Phonebook.GetContactNumbers")]
		public IHttpActionResult GetContactNumbers([FromODataUri]Guid key)
		{
			var items = _service.GetContactNumbers(key).ToList();

			return Ok(items);
		}
	}
}
