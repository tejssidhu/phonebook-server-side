﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.WebApi.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using Phonebook.Domain.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.OData.Results;
using Moq;
using System.Linq;

namespace Phonebook.WebApi.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
    public class TestContactsController : BaseApiTests
    {
        [TestMethod]
        public void GetReturnsContacts()
        {
            ContactsController controller = new ContactsController(MockContactService.Object);

            var response = controller.Get();
			var results = response.ToList();

			Assert.IsNotNull(results);
			Assert.AreEqual(52, results.Count);
        }

        [TestMethod]
        public void GetExistingContactReturnsContact()
        {
            var guidOfExistingContact = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            ContactsController controller = new ContactsController(MockContactService.Object);

			SingleResult<Contact> response = controller.Get(guidOfExistingContact);
			var contact = (response.Queryable).FirstOrDefault();

			Assert.AreEqual(guidOfExistingContact, contact.Id);
		}

        [TestMethod]
        public void GetContactThatDoesntExistReturnsNotFound()
        {
            var guidOfContact = new Guid("b224a9a9-f02f-4403-ba6c-fb05951ede65");
            ContactsController controller = new ContactsController(MockContactService.Object);

			SingleResult<Contact> response = controller.Get(guidOfContact);
			var contact = (response.Queryable).FirstOrDefault();

			Assert.IsNull(contact);
		}

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var newTitle = "Mr";
            var controller = new ContactsController(MockContactService.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Contact { Title = newTitle });
            var createdResult = actionResult as CreatedODataResult<Contact>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Entity.Id);
            Assert.AreEqual(createdResult.Entity.Title, newTitle);
        }

        [TestMethod]
        public void PostMethodReturnsBadRequestWhenModelIsInvalid()
        {
            // Arrange
            var newContactGuid = new Guid("0998d643-4c5e-4b1f-9778-e0f6974eaf1d");
            var newTitle = "Mr";
            var controller = new ContactsController(MockContactService.Object);
            controller.ModelState.AddModelError("test", "test");

            // Act
            IHttpActionResult actionResult = controller.Post(new Contact { Id = newContactGuid, Title = newTitle });
            var createdResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        public void PutReturnsContentResult()
        {
            // Arrange
            var guidOfExistingContact = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            var controller = new ContactsController(MockContactService.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingContact, new Contact { Id = guidOfExistingContact, Title = "Mrs" });
            var contentResult = actionResult as OkNegotiatedContentResult<Contact>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(guidOfExistingContact, contentResult.Content.Id);
        }
        
        [TestMethod]
        public void PutReturnsBadRequestWhenModelIsInvalidResult()
        {
            // Arrange
            var guidOfExistingContact = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            var controller = new ContactsController(MockContactService.Object);
            controller.ModelState.AddModelError("test", "test");

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingContact, new Contact { Id = guidOfExistingContact, Title = "Mrs" });
            var createdResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PutThrowsException()
        {
            // Arrange
            var guidOfExistingContact = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            MockContactService.Setup(item => item.Update(It.IsAny<Contact>())).Throws(new Exception());
            var controller = new ContactsController(MockContactService.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingContact, new Contact { Id = guidOfExistingContact, Title = "Mrs" });
            
            // Assert
        }

        [TestMethod]
        public void PutReturnsBadRequestResultWhenGuidDontMatch()
        {
            // Arrange
            var guidOfExistingContact = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            var controller = new ContactsController(MockContactService.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingContact, new Contact { Id = Guid.Empty, Title = "Mrs" });
            var contentResult = actionResult as BadRequestResult;

            // Assert
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            var guidOfExistingContact = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            var controller = new ContactsController(MockContactService.Object);

            IHttpActionResult response = controller.Delete(guidOfExistingContact);
            var statusCodeResult = response as StatusCodeResult;

            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(statusCodeResult.StatusCode, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteContactThatDoesntExistReturnsNotFound()
        {
            var guidOfContact = new Guid("b224a9a9-f02f-4403-ba6c-fb05951ede65");
            var controller = new ContactsController(MockContactService.Object);

            IHttpActionResult response = controller.Delete(guidOfContact);

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

		[TestMethod]
		public void GetContactNumbersByContactId()
		{
			var guidOfExistingUser = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
			var controller = new ContactsController(MockContactService.Object);

			var response = controller.GetContactNumbers(guidOfExistingUser);
			var results = response.ToList();

			Assert.IsNotNull(results);
			Assert.AreEqual(2, results.Count);
		}
	}
}
