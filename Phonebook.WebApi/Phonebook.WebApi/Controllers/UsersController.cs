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
    public class UsersController : ODataController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        public IHttpActionResult Get()
        {
            var result = new List<User>();
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

        [HttpPost]
        [HttpOptions]
        [ODataRoute("Authenticate")]
        public IHttpActionResult Authenticate(ODataActionParameters parameters)
        {
            object username;
            object password;
            if (!parameters.TryGetValue("username", out username) || !parameters.TryGetValue("password", out password))
            {
                return NotFound();
            }
            User item;
            try
            {
                item = _service.Authenticate(username.ToString(), password.ToString());
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidPasswordException)
            {
                return NotFound();
            }

            return Ok(item);
        }

        public IHttpActionResult Post(User entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Guid userId = _service.Create(entity);
            entity.Id = userId;

            return Created(entity);
        }

        public IHttpActionResult Put([FromODataUri] Guid key, User entity)
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
            var contact = _service.Get(key);
            if (contact == null)
            {
                return NotFound();
            }
            _service.Delete(key);
            return StatusCode(HttpStatusCode.NoContent);
        }

		[Authorize]
		[ScopeAuthorise("phonebookAPI.read")]
		[HttpGet]
        [ODataRoute("Users({key})/Phonebook.MyContacts")]
        public IHttpActionResult MyContacts([FromODataUri]Guid key)
        {
            var result = new List<Contact>();
            var items = _service.GetContacts(key).ToList();

            return Ok(items);
        }
    }
}
