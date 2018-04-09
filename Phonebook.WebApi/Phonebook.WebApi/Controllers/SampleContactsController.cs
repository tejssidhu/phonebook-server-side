using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.OData;

namespace Phonebook.WebApi.Controllers
{
	public class SampleContactsController : ODataController
	{
		private readonly ISampleContactService _service;

		public SampleContactsController(ISampleContactService service)
		{
			_service = service;
		}

		[EnableQuery(PageSize = 10)]
		public IQueryable<SampleContact> Get()
		{
			return _service.GetAll();
		}

		[EnableQuery]
		public SingleResult<SampleContact> Get([FromODataUri]Guid key)
		{
			return SingleResult.Create(_service.Get(key));
		}

		public IHttpActionResult Post(SampleContact entity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			entity.Id = Guid.NewGuid();
			_service.Create(entity);

			return Created(entity);
		}

		public IHttpActionResult Put([FromODataUri] Guid key, SampleContact entity)
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
