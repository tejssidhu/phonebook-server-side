using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using Phonebook.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;

namespace Phonebook.WebApi.Controllers
{
	public class SampleUsersController : ODataController
	{
		private readonly ISampleUserService _service;

		public SampleUsersController(ISampleUserService service)
		{
			_service = service;
		}

		[EnableQuery(PageSize = 10)]
		public IQueryable<SampleUser> Get()
		{
			return _service.GetAll();
		}

		[EnableQuery]
		public SingleResult<SampleUser> Get([FromODataUri]Guid key)
		{
			var item = _service.Get(key);

			return SingleResult.Create(item);
		}

		public IHttpActionResult Post(SampleUser entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			Guid userId = _service.Create(entity);
			entity.Id = userId;

			return Created(entity);
		}

		public IHttpActionResult Put([FromODataUri] Guid key, SampleUser entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (key != entity.Id)
			{
				return BadRequest();
			}

			try
			{
				_service.Update(entity);
			}
			catch
			{
				throw;
			}

			return Ok(entity);
		}

		public IHttpActionResult Delete([FromODataUri] Guid key)
		{
			var contact = _service.Get(key).FirstOrDefault();
			if (contact == null)
			{
				return NotFound();
			}
			_service.Delete(key);
			return StatusCode(HttpStatusCode.NoContent);
		}

		[EnableQuery(PageSize = 10)]
		public IQueryable<SampleContact> GetSampleContacts([FromODataUri] Guid key)
		{
			return _service.GetAll().Where(u => u.Id == key).SelectMany(u => u.SampleContacts);
		}
	}
}
