using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.Domain.Model;

namespace Phonebook.Data.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class UserRepositoryTests : BaseRespositoryTests
	{
		[TestMethod]
		public void GetAllOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var userList = new List<User>(_users.OrderBy(c => c.Username));

			//Act
			List<User> users = unitOfWork.UserRepository.GetAll().ToList();

			//Assert
			CollectionAssert.AreEqual(userList, users.OrderBy(c => c.Username).ToList());
		}

		[TestMethod]
		public void GetOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var userList = new List<User>(_users);

			//Act
			User user = unitOfWork.UserRepository.Get(new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf"));

			//Assert
			Assert.AreEqual(userList[0], user);
			unitOfWork.Dispose();
		}

		[TestMethod]
		public void CreateOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);

			var userToCreate = new User
			{
				Username = "Tej.Sidhu"
			};

			//Act
			unitOfWork.UserRepository.Create(userToCreate);
			unitOfWork.SaveChanges();
			User user = unitOfWork.UserRepository.Get(userToCreate.Id);

			//Assert
			Assert.AreEqual(UnProxy(user), userToCreate);
			Assert.IsNotNull(userToCreate.Id);
		}

		[TestMethod]
		public void UpdateOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var userList = new List<User>(_users);

			var userToUpdate = userList[1];

			//Act
			unitOfWork.UserRepository.Update(userToUpdate);
			unitOfWork.SaveChanges();
			User user = unitOfWork.UserRepository.Get(userToUpdate.Id);

			//Assert
			Assert.AreEqual(UnProxy(user), userToUpdate);
		}

		[TestMethod]
		public void DeleteOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var userList = new List<User>(_users);
			var userToDelete = userList[1];

			//Act
			unitOfWork.UserRepository.Delete(userToDelete.Id);
			unitOfWork.SaveChanges();
			User user = unitOfWork.UserRepository.Get(userToDelete.Id);

			//Assert
			Assert.IsNull(user);
		}

		[TestMethod]
		public void DeleteMultipleOnUserRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);

			//Act
			unitOfWork.UserRepository.DeleteMany(t => t.Username.Contains("User"));
			unitOfWork.SaveChanges();

			bool anyStillExist = unitOfWork.UserRepository.GetAll(t => t.Username.Contains("User")).Any();
			
			//Assert
			Assert.AreEqual(anyStillExist, false);
		}


		private User UnProxy(dynamic proxiedType)
		{
			User user = new User();

			user.Id = proxiedType.Id;
			user.Username = proxiedType.Username;

			return user;
		}
	}

}
