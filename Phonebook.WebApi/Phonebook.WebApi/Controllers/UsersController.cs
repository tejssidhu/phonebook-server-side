﻿using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
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

        [HttpGet]
        [ODataRoute("Authenticate(username={username}, password={password})")]
        public IHttpActionResult Authenticate([FromODataUri]string username, [FromODataUri]string password)
        {
            User item;
            try
            {
                item = _service.Authenticate(username, password);
            }
            catch (ObjectNotFoundException e)
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
            Guid contactId = _service.Create(entity);

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
