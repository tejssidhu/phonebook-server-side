using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Model;
using Phonebook.Domain.Services;

namespace Phonebook.Domain.Tests
{
	[TestClass]
	public class UserServiceTests : BaseDomainTests
	{

		#region Test Initialise and Cleanup
		#endregion

		[TestMethod]
		public void GetAllOnUserService()
		{
			//arrange
			UserService userService = new UserService(mockUnitOfWork.Object);

			//act
			List<User> retUsers = userService.GetAll().ToList();

			//assert
			CollectionAssert.AreEqual(_users, retUsers);

			userService.Dispose();
		}

		[TestMethod]
		public void GetOnUserService()
		{
			//arrange
			Guid id = _user.Id;
			UserService userService = new UserService(mockUnitOfWork.Object);

			//act
			User retUser = userService.Get(id);

			//assert
			Assert.AreEqual(_user, retUser);

			userService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidPasswordException))]
		public void AuthenticateWithInvalidPasswordOnUserService()
		{
			//arrange
			UserService userService = new UserService(mockUnitOfWork.Object);

			//act
			User retUser = userService.Authenticate(_user.Username, _user.Password + "WRONG");

			//assert - expect exception

			userService.Dispose();
		}

		[TestMethod]
		public void AuthenticateValidPasswordOnUserService()
		{
			//arrange
			UserService userService = new UserService(mockUnitOfWork.Object);

			//act
			User retUser = userService.Authenticate(_user.Username, _user.Password);

			//assert
			Assert.AreEqual(_user, retUser);

			userService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectNotFoundException))]
		public void AuthenticateWithNoExistentUserOnUserService()
		{
			//arrange
			UserService userService = new UserService(mockUnitOfWork.Object);

			//act
			User retUser = userService.Authenticate(_user.Username + "DOESNTEXIST", _user.Password);

			//assert - expect exception

			userService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectAlreadyExistException))]
		public void CreateWithExistingUserOnUserService()
		{
			//arrange
			UserService userService = new UserService(mockUnitOfWork.Object);

			//act
			Guid id = userService.Create(_user);

			//assert - expect exception

			userService.Dispose();
		}

		[TestMethod]
		public void CreateOnUserService()
		{
			//arrange
			User userToCreate = new User
			{
				Id = new Guid("0b21d4b6-eb42-456b-9828-a90cb604bceb"),
				Password = "7BbfOOoMJCf",
				Username = "igardner8"
			};

			UserService userService = new UserService(mockUnitOfWork.Object);

			//act
			Guid id = userService.Create(userToCreate);

			//assert
			mockUserRepository.Verify(y => y.Create(It.IsAny<User>()));
			Assert.IsNotNull(id);
			Assert.AreEqual(id, userToCreate.Id);

			userService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectAlreadyExistException))]
		public void UpdateToExistingUsernameOnUserService()
		{
			//arrange
			UserService userService = new UserService(mockUnitOfWork.Object);

			//set username to that of another user
			_user.Username = _users[0].Username;

			//act
			userService.Update(_user);

			//assert - expected exception

			userService.Dispose();
		}

		[TestMethod]
		public void UpdateOnUserService()
		{
			//arrange
			UserService userService = new UserService(mockUnitOfWork.Object);

			//set username to that of another user
			_user.Username = _user.Username + "WITHUPDATE";

			//act
			userService.Update(_user);

			//assert - expected exception
			mockUserRepository.Verify(y => y.Update(It.IsAny<User>()));

			userService.Dispose();
		}

		[TestMethod]
		public void DeleteOnUserService()
		{
			//arrange
			UserService userService = new UserService(mockUnitOfWork.Object);

			//act
			userService.Delete(_user.Id);

			//assert - expected exception
			mockUserRepository.Verify(y => y.Delete(It.IsAny<Guid>()));

			userService.Dispose();
		}
	}
}
