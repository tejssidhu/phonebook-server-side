using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Domain.Model;
using Phonebook.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.OData.Results;

namespace Phonebook.WebApi.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
    public class TestUsersController : BaseApiTests
    {
        [TestMethod]
        public void GetReturnsUsers()
        {
            UsersController controller = new UsersController(MockUserService.Object);

            var response = controller.Get();
			var results = response.ToList();

            Assert.IsNotNull(results);
            Assert.AreEqual(11, results.Count);
        }
        
        [TestMethod]
        public void GetExistingUserReturnsUser()
        {
            var guidOfExisting = new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf");
            UsersController controller = new UsersController(MockUserService.Object);

            SingleResult<User> response = controller.Get(guidOfExisting);
			var user = (response.Queryable).FirstOrDefault();

			Assert.AreEqual(guidOfExisting, user.Id);
		}

        [TestMethod]
        public void GetUserThatDoesntExistReturnsNotFound()
        {
            var guid = new Guid("b224a9a9-f02f-4403-ba6c-fb05951ede65");
            UsersController controller = new UsersController(MockUserService.Object);

            SingleResult<User> response = controller.Get(guid);
			var user = (response.Queryable).FirstOrDefault();

			Assert.IsNull(user);
        }
        
        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var newGuid = new Guid("0998d643-4c5e-4b1f-9778-e0f6974eaf1d");
            var newUsername = "User name 1";
            var controller = new UsersController(MockUserService.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new User { Id = newGuid, Username = newUsername });
            var createdResult = actionResult as CreatedODataResult<User>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(createdResult.Entity.Id, newGuid);
            Assert.AreEqual(createdResult.Entity.Username, newUsername);
        }

        [TestMethod]
        public void PostMethodReturnsBadRequestWhenModelIsInvalid()
        {
            // Arrange
            var newGuid = new Guid("0998d643-4c5e-4b1f-9778-e0f6974eaf1d");
            var newUsername = "User name 1";
            var controller = new UsersController(MockUserService.Object);
            controller.ModelState.AddModelError("test", "test");

            // Act
            IHttpActionResult actionResult = controller.Post(new User { Id = newGuid, Username = newUsername });
            var createdResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        public void PutReturnsContentResult()
        {
            // Arrange
            var guidOfExistingUser = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            var userName = "User name 1";
            var controller = new UsersController(MockUserService.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingUser, new User { Id = guidOfExistingUser, Username = userName });
            var contentResult = actionResult as OkNegotiatedContentResult<User>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(guidOfExistingUser, contentResult.Content.Id);
            Assert.AreEqual(userName, contentResult.Content.Username);
        }

        [TestMethod]
        public void PutReturnsBadRequestWhenModelIsInvalidResult()
        {
            // Arrange
            var guidOfExistingUser = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            var userName = "User name 1";
            var controller = new UsersController(MockUserService.Object);
            controller.ModelState.AddModelError("test", "test");

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingUser, new User { Id = guidOfExistingUser, Username = userName });
            var createdResult = actionResult as InvalidModelStateResult;

            // Assert
            Assert.IsNotNull(createdResult);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PutThrowsException()
        {
            // Arrange
            var guidOfExistingUser = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            var userName = "User name 1";
            MockUserService.Setup(item => item.Update(It.IsAny<User>())).Throws(new Exception());
            var controller = new UsersController(MockUserService.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingUser, new User { Id = guidOfExistingUser, Username = userName });

            // Assert
        }

        [TestMethod]
        public void PutReturnsBadRequestResultWhenGuidDontMatch()
        {
            // Arrange
            var guidOfExistingUser = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383");
            var userName = "User name 1";
            var controller = new UsersController(MockUserService.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(guidOfExistingUser, new User { Id = Guid.Empty, Username = userName });
            var contentResult = actionResult as BadRequestResult;

            // Assert
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            var guidOfExistingUser = new Guid("5875412f-e8b8-493e-bd58-5df35083342c");
            var controller = new UsersController(MockUserService.Object);

            IHttpActionResult response = controller.Delete(guidOfExistingUser);
            var statusCodeResult = response as StatusCodeResult;

            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(statusCodeResult.StatusCode, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteContactThatDoesntExistReturnsNotFound()
        {
            var guid = new Guid("b224a9a9-f02f-4403-ba6c-fb05951ede65");
            var controller = new UsersController(MockUserService.Object);

            IHttpActionResult response = controller.Delete(guid);

            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

		[TestMethod]
		public void GetContactsByUserId()
		{
			var guidOfExistingUser = new Guid("5875412f-e8b8-493e-bd58-5df35083342c");
			var controller = new UsersController(MockUserService.Object);

			var response = controller.GetContacts(guidOfExistingUser);
			var results = response.ToList();

			Assert.IsNotNull(results);
			Assert.AreEqual(4, results.Count);
		}
	}
}
