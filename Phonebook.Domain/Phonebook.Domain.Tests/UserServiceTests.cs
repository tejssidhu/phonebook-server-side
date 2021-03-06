﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Model;
using Phonebook.Domain.Services;

namespace Phonebook.Domain.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class UserServiceTests : BaseDomainTests
	{

		#region Test Initialise and Cleanup
		#endregion

		[TestMethod]
		public void GetAllOnUserService()
		{
			//arrange
			UserService userService = new UserService(MockUnitOfWork.Object);

			//act
			List<User> retUsers = userService.GetAll().ToList();

			//assert
			CollectionAssert.AreEqual(testContext.Users, retUsers);

			userService.Dispose();
		}

		[TestMethod]
		public void GetOnUserService()
		{
			//arrange
			Guid id = testContext.SingleUser.Id;
			UserService userService = new UserService(MockUnitOfWork.Object);

			//act
			User retUser = userService.Get(id).FirstOrDefault();

			//assert
			Assert.AreEqual(testContext.SingleUser, retUser);

			userService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectAlreadyExistException))]
		public void CreateWithExistingUserOnUserService()
		{
			//arrange
			UserService userService = new UserService(MockUnitOfWork.Object);

			//act
			Guid id = userService.Create(testContext.SingleUser);

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
				Username = "igardner8"
			};

			UserService userService = new UserService(MockUnitOfWork.Object);

			//act
			Guid id = userService.Create(userToCreate);

			//assert
			MockUserRepository.Verify(y => y.Create(It.IsAny<User>()));
			Assert.IsNotNull(id);
			Assert.AreEqual(id, userToCreate.Id);

			userService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectAlreadyExistException))]
		public void UpdateToExistingUsernameOnUserService()
		{
			//arrange
			UserService userService = new UserService(MockUnitOfWork.Object);

			//set username to that of another user
			testContext.SingleUser.Username = testContext.Users[0].Username;

			//act
			userService.Update(testContext.SingleUser);

			//assert - expected exception

			userService.Dispose();
		}

		[TestMethod]
		public void UpdateOnUserService()
		{
			//arrange
			UserService userService = new UserService(MockUnitOfWork.Object);

			//set username to that of another user
			testContext.SingleUser.Username = testContext.SingleUser.Username + "WITHUPDATE";

			//act
			userService.Update(testContext.SingleUser);

			//assert - expected exception
			MockUserRepository.Verify(y => y.Update(It.IsAny<User>()));

			userService.Dispose();
		}

		[TestMethod]
		public void DeleteOnUserService()
		{
			//arrange
			UserService userService = new UserService(MockUnitOfWork.Object);

			//act
			userService.Delete(testContext.SingleUser.Id);

			//assert - expected exception
			MockUserRepository.Verify(y => y.Delete(It.IsAny<Guid>()));

			userService.Dispose();
		}
	}
}
