using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Domain.Model;
using Phonebook.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.OData.Results;

namespace Phonebook.WebApi.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
    public class TestContactNumbersController: BaseApiTests
    {
        [TestMethod]
        public void GetReturnsContactNumbers()
        {
            ContactNumbersController controller = new ContactNumbersController(MockContactNumberService.Object);

            IHttpActionResult response = controller.Get();
            var contentResult = response as OkNegotiatedContentResult<List<ContactNumber>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(16, contentResult.Content.Count);
        }

        [TestMethod]
        public void GetExistingContactNumberReturnsContactNumber()
        {
            var guidOfExisting = new Guid("9a005b3e-d9ec-4e08-aefa-589ab5e00bfa");
            ContactNumbersController controller = new ContactNumbersController(MockContactNumberService.Object);

            IHttpActionResult response = controller.Get(guidOfExisting);
            var contentResult = response as OkNegotiatedContentResult<ContactNumber>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(guidOfExisting, contentResult.Content.Id);
        }

        [TestMethod]
        public void GetContactNumberThatDoesntExistReturnsNotFound()
        {
            var guid = new Guid("b224a9a9-f02f-4403-ba6c-fb05951ede65");
            ContactNumbersController controller = new ContactNumbersController(MockContactNumberService.Object);

            IHttpActionResult response = controller.Get(guid);

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var newGuid = new Guid("9a005b3e-d9ec-4e08-aefa-589ab5e00bfa");
            var newDesc = "Description";
            var controller = new ContactNumbersController(MockContactNumberService.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new ContactNumber { Id = newGuid, Description = newDesc });
            var createdResult = actionResult as CreatedODataResult<ContactNumber>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(createdResult.Entity.Id, newGuid);
            Assert.AreEqual(createdResult.Entity.Description, newDesc);
        }

        [TestMethod]
        public void PostMethodReturnsBadRequestWhenModelIsInvalid()
        {
            // Arrange
            var newGuid = new Guid("0998d643-4c5e-4b1f-9778-e0f6974eaf1d");
            var newDesc = "Description";
            var controller = new ContactNumbersController(MockContactNumberService.Object);
            controller.ModelState.AddModelError("test", "test");

            // Act
            IHttpActionResult actionResult = controller.Post(new ContactNumber { Id = newGuid, Description = newDesc });
            var createdResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(createdResult);
        }


        [TestMethod]
        public void PutReturnsContentResult()
        {
            // Arrange
            var guidOfExistingContactNumber = new Guid("9a005b3e-d9ec-4e08-aefa-589ab5e00bfa");
            var newDesc = "Description";
            var controller = new ContactNumbersController(MockContactNumberService.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingContactNumber, new ContactNumber { Id = guidOfExistingContactNumber, Description = newDesc });
            var contentResult = actionResult as OkNegotiatedContentResult<ContactNumber>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(guidOfExistingContactNumber, contentResult.Content.Id);
            Assert.AreEqual(newDesc, contentResult.Content.Description);
        }

        [TestMethod]
        public void PutReturnsBadRequestWhenModelIsInvalidResult()
        {
            // Arrange
            var guidOfExistingContactNumber = new Guid("9a005b3e-d9ec-4e08-aefa-589ab5e00bfa");
            var newDesc = "Description";
            var controller = new ContactNumbersController(MockContactNumberService.Object);
            controller.ModelState.AddModelError("test", "test");

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingContactNumber, new ContactNumber { Id = guidOfExistingContactNumber, Description = newDesc });
            var createdResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PutThrowsException()
        {
            // Arrange
            var guidOfExistingContactNumber = new Guid("9a005b3e-d9ec-4e08-aefa-589ab5e00bfa");
            var newDesc = "Description";
            MockContactNumberService.Setup(item => item.Update(It.IsAny<ContactNumber>())).Throws(new Exception());
            var controller = new ContactNumbersController(MockContactNumberService.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingContactNumber, new ContactNumber { Id = guidOfExistingContactNumber, Description = newDesc });

            // Assert
        }

        [TestMethod]
        public void PutReturnsBadRequestResultWhenGuidDontMatch()
        {
            // Arrange
            var guidOfExistingContactNumber = new Guid("9a005b3e-d9ec-4e08-aefa-589ab5e00bfa");
            var newDesc = "Description";
            var controller = new ContactNumbersController(MockContactNumberService.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingContactNumber, new ContactNumber { Id = Guid.Empty, Description = newDesc });
            var contentResult = actionResult as BadRequestResult;

            // Assert
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            var guidOfExistingContactNumber = new Guid("9a005b3e-d9ec-4e08-aefa-589ab5e00bfa");
            var controller = new ContactNumbersController(MockContactNumberService.Object);

            IHttpActionResult response = controller.Delete(guidOfExistingContactNumber);
            var statusCodeResult = response as StatusCodeResult;

            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(statusCodeResult.StatusCode, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteContactThatDoesntExistReturnsNotFound()
        {
            var guid = new Guid("b224a9a9-f02f-4403-ba6c-fb05951ede65");
            var controller = new ContactNumbersController(MockContactNumberService.Object);

            IHttpActionResult response = controller.Delete(guid);

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }
    }
}
