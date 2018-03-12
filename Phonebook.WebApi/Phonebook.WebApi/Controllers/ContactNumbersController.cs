using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using Phonebook.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData;

namespace Phonebook.WebApi.Controllers
{
	[Authorize]
	public class ContactNumbersController : ODataController
	{
		private readonly IContactNumberService _service;

		public ContactNumbersController(IContactNumberService service)
		{
			_service = service;
		}

		[EnableQuery(PageSize = 10)]
		[ScopeAuthorise("phonebookAPI.read")]
		public IQueryable<ContactNumber> Get()
		{
			return _service.GetAll();
		}

		[ScopeAuthorise("phonebookAPI.read")]
		public SingleResult<ContactNumber> Get([FromODataUri]Guid key)
		{
			return SingleResult.Create(_service.Get(key));
		}

		[ScopeAuthorise("phonebookAPI.write")]
		public IHttpActionResult Post(ContactNumber entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			entity.Id = Guid.NewGuid();
			_service.Create(entity);

			return Created(entity);
		}

		[ScopeAuthorise("phonebookAPI.write")]
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

		[ScopeAuthorise("phonebookAPI.write")]
		public IHttpActionResult Delete([FromODataUri] Guid key)
		{
			var contactNumber = _service.Get(key).FirstOrDefault();
			if (contactNumber == null)
			{
				return NotFound();
			}

			_service.Delete(key);
			return StatusCode(System.Net.HttpStatusCode.NoContent);
		}
	}
}
