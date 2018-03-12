using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.Domain.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Phonebook.Data.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class ContactRepositoryTests : BaseRespositoryTests
	{

		[TestMethod]
		public void GetAllOnContactRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactsList = new List<Contact>(_contacts.OrderBy(c => c.Surname).ThenBy(n => n.Forename));

			//Act
			List<Contact> contacts = unitOfWork.ContactRepository.GetAll().ToList();

			List<Contact> unProxiedContacts = new List<Contact>();
			foreach (var c in contacts)
			{
				unProxiedContacts.Add(UnProxy(c));
			}

			unProxiedContacts = unProxiedContacts.OrderBy(c => c.Surname).ThenBy(n => n.Forename).ToList();

			//Assert
			CollectionAssert.AreEqual(contactsList, unProxiedContacts);

			unitOfWork.Dispose();
		}

		[TestMethod]
		public void GetOnContactRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactsList = new List<Contact>(_contacts);

			//Act
			Contact contact = unitOfWork.ContactRepository.Get(new Guid("81c4763c-b225-4756-903a-750064167813")).FirstOrDefault();

			contact = UnProxy(contact);

			//Assert
			Assert.AreEqual(contactsList[0], contact);

			unitOfWork.Dispose();
		}

		[TestMethod]
		public void CreateOnContactRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactsList = new List<Contact>(_contacts);

			var contactToCreate = new Contact
			{
				UserId = contactsList[0].Id,
				Title = "Mrs",
				Forename = "Maggie",
				Surname = "Thatcher",
				Email = "Maggie.Thatcher@goo.gl"
			};

			//Act
			unitOfWork.ContactRepository.Create(contactToCreate);
			unitOfWork.SaveChanges();

			Contact contact = unitOfWork.ContactRepository.Get(contactToCreate.Id).FirstOrDefault();

			//Assert
			Assert.AreEqual(contact, contactToCreate);
			Assert.IsNotNull(contactToCreate.Id);

			unitOfWork.Dispose();
		}

		[TestMethod]
		public void UpdateOnContactRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactsList = new List<Contact>(_contacts);
			var contactToUpdate = contactsList[3];
			contactToUpdate.UserId = new Guid("7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf");
			contactToUpdate.Forename = "Sean" + "Updated";
			contactToUpdate.Surname = "Baker" + "Updated";
			contactToUpdate.Email = "sbaker3@noaa.gov" + "Updated";
			contactToUpdate.Title = "Honorable" + "Updated";

			//Act
			unitOfWork.ContactRepository.Update(contactToUpdate);
			unitOfWork.SaveChanges();

			Contact contact = unitOfWork.ContactRepository.Get(contactToUpdate.Id).FirstOrDefault();

			contact = UnProxy(contact);

			//Assert
			Assert.AreEqual(contact, contactToUpdate);

			unitOfWork.Dispose();
		}

		[TestMethod]
		public void DeleteOnContactRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactsList = new List<Contact>(_contacts);
			var contactToDelete = contactsList[4];

			//Act
			unitOfWork.ContactRepository.Delete(contactToDelete.Id);
			unitOfWork.SaveChanges();

			//Assert
			Assert.IsFalse(_contacts.Any(x => x.Id == contactToDelete.Id));
		}

		private Contact UnProxy(dynamic proxiedType)
		{
			Contact contact = new Contact();

			contact.Id = proxiedType.Id;
			contact.Surname = proxiedType.Surname;
			contact.Title = proxiedType.Title;
			contact.UserId = proxiedType.UserId;
			contact.Forename = proxiedType.Forename;
			contact.Email = proxiedType.Email;

			return contact;
		}
	}
}
