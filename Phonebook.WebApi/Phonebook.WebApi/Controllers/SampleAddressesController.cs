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
	public class SampleAddressesController : ODataController
	{
		private readonly ISampleAddressService _service;

		public SampleAddressesController(ISampleAddressService service)
		{
			_service = service;
		}

		[EnableQuery(PageSize = 10)]
		public IQueryable<SampleAddress> Get()
		{
			return _service.GetAll();
		}

		[EnableQuery]
		public SingleResult<SampleAddress> Get([FromODataUri]Guid key)
		{
			return SingleResult.Create(_service.Get(key));
		}

		public IHttpActionResult Post(SampleAddress entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			entity.Id = Guid.NewGuid();
			_service.Create(entity);

			return Created(entity);
		}

		public IHttpActionResult Put([FromODataUri] Guid key, SampleAddress entity)
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
			var entity = _service.Get(key).FirstOrDefault();
			if (entity == null)
			{
				return NotFound();
			}

			_service.Delete(key);
			return StatusCode(System.Net.HttpStatusCode.NoContent);
		}
	}
}
