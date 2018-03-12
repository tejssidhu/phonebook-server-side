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

		[EnableQuery(PageSize = 10)]
		[ScopeAuthorise("phonebookAPI.read")]
		public IQueryable<Contact> Get()
		{
			return _service.GetAll();
		}

		[EnableQuery]
		[ScopeAuthorise("phonebookAPI.read")]
		public SingleResult<Contact> Get([FromODataUri]Guid key)
		{
			return SingleResult.Create(_service.Get(key));
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
			var contact = _service.Get(key).FirstOrDefault();
			if (contact == null)
			{
				return NotFound();
			}
			_service.Delete(key);
			return StatusCode(System.Net.HttpStatusCode.NoContent);
		}

		[EnableQuery(PageSize = 10)]
		public IQueryable<ContactNumber> GetContactNumbers([FromODataUri]Guid key)
		{
			return _service.GetAll().Where(u => u.Id == key).SelectMany(u => u.ContactNumbers);
		}
		
	}
}
