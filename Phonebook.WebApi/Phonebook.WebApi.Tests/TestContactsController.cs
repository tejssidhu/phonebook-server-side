using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.WebApi.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using Phonebook.Domain.Model;
using System.Collections.Generic;
using System.Web.OData.Results;

namespace Phonebook.WebApi.Tests
{
    [TestClass]
    public class TestContactsController : BaseApiTests
    {
        [TestMethod]
        public void GetReturnsContacts()
        {
            ContactsController controller = new ContactsController(MockContactService.Object);

            IHttpActionResult response = controller.Get();
            var contentResult = response as OkNegotiatedContentResult<List<Contact>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(52, contentResult.Content.Count);
        }

        [TestMethod]
        public void GetExistingContactReturnsContact()
        {
            var guidOfExistingContact = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            ContactsController controller = new ContactsController(MockContactService.Object);

            IHttpActionResult response = controller.Get(guidOfExistingContact);
            var contentResult = response as OkNegotiatedContentResult<Contact>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(guidOfExistingContact, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetContactThatDoesntExistReturnsNotFound()
        {
            var guidOfContact = new Guid("b224a9a9-f02f-4403-ba6c-fb05951ede65");
            ContactsController controller = new ContactsController(MockContactService.Object);

            IHttpActionResult response = controller.Get(guidOfContact);

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var newContactGuid = new Guid("0998d643-4c5e-4b1f-9778-e0f6974eaf1d");
            var newTitle = "Mr";
            var controller = new ContactsController(MockContactService.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Contact { Id = newContactGuid, Title = newTitle });
            var createdResult = actionResult as CreatedODataResult<Contact>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(createdResult.Entity.Id, newContactGuid);
            Assert.AreEqual(createdResult.Entity.Title, newTitle);
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

        //[TestMethod]
        //public void GetByUserIdReturnsCorrectContacts()
        //{
        //    var existingUser = new Guid("26e31dde-4bcb-47d4-be80-958676c5cafd");
        //    ContactsController controller = new ContactsController(MockContactService.Object);

        //    IHttpActionResult response = controller.GetByUser(existingUser);
        //    var contentResult = response as OkResult<List<Contact>>;

        //    Assert.IsNotNull(contentResult);
        //    Assert.IsNotNull(contentResult.Content);
        //    Assert.AreEqual(3, contentResult.Content.Count);
        //}
    }
}
