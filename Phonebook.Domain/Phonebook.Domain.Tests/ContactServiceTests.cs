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
	public class ContactServiceTests : BaseDomainTests
	{
		#region Test Initialise and Cleanup

		#endregion

		#region tests
		[TestMethod]
		[ExpectedException(typeof(ObjectAlreadyExistException))]
		public void CreateContactForUserWithEmailAddressOfExistingContactOnContactService()
		{
			//arrange
			Contact contactToCreate = new Contact
			{
				UserId = new Guid("26e31dde-4bcb-47d4-be80-958676c5cafd"),
				Title = "Dr",
				Email = "treyes0@goo.gl",
				Forename = "T",
				Surname = "Reyes"
			};

			ContactService contactService = new ContactService(MockUnitOfWork.Object);

			//act
			Guid contactId = contactService.Create(contactToCreate);

			//assert - expect excpetion
			contactService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectNotFoundException))]
		public void CreateNewContactForNoneExistentUserOnContactService()
		{
			//arrange
			var contactToCreate = new Contact { UserId = new Guid("318274f0-573c-416b-aa4b-b68b83ec8427"), Forename = "Carlos", Surname = "Daniels", Email = "cdaniels1h@tripod.com", Title = "Mr" };

			ContactService contactService = new ContactService(MockUnitOfWork.Object);

			//act
			Guid contactId = contactService.Create(contactToCreate);

			//assert - expected exception
			contactService.Dispose();
		}

		[TestMethod]
		public void CreateNewContactOnContactService()
		{
			//arrange
			var contactToCreate = new Contact { UserId = new Guid("0d1a6711-e9eb-418e-adda-47a62a7900c9"), Forename = "Carlos", Surname = "Daniels", Email = "cdaniels1h@tripod.com", Title = "Mr" };

			ContactService contactService = new ContactService(MockUnitOfWork.Object);

			//act
			Guid contactId = contactService.Create(contactToCreate);

			//assert
			MockContactRepository.Verify(y => y.Create(It.IsAny<Contact>()), Times.Once);

			contactService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectAlreadyExistException))]
		public void UpdateContactToExistingContactsEmailOnContactService()
		{
			//arrange
			var contactToUpdate = testContext.SingleUser.PhoneBook.Where((x, i) => i == 0).FirstOrDefault(); //testContext.SingleUser.PhoneBook[0];

			ContactService contactService = new ContactService(MockUnitOfWork.Object);

			//set email to that of another contact in that users Phonebook
			contactToUpdate.Email = testContext.SingleUser.PhoneBook.Where((x, i) => i == 1).FirstOrDefault().Email;

			//act
			contactService.Update(contactToUpdate);

			//assert - expected exception

			contactService.Dispose();
		}

		[TestMethod]
		public void UpdateContactOnContactService()
		{
			//arrange
			ContactService contactService = new ContactService(MockUnitOfWork.Object);

			//set username to that of another user
			testContext.SingleContact.Email = testContext.SingleContact.Email + "WITHUPDATE";

			//act
			contactService.Update(testContext.SingleContact);

			//assert
			MockContactRepository.Verify(y => y.Update(It.IsAny<Contact>()), Times.Once);

			contactService.Dispose();
		}

		[TestMethod]
		public void DeleteContactOnContactService()
		{
			//arrange
			ContactService contactService = new ContactService(MockUnitOfWork.Object);

			//act
			contactService.Delete(testContext.SingleContact.Id);

			//assert - expected exception
			MockContactRepository.Verify(y => y.Delete(It.IsAny<Guid>()), Times.Once);

			contactService.Dispose();
		}

		[TestMethod]
		public void SearchContactsByEmailOnContactService()
		{
			//arrange
			ContactService contactService = new ContactService(MockUnitOfWork.Object);

			//act
			List<Contact> retContacts = contactService.SearchContactsByEmail(testContext.SingleUser.Id, "treyes0@").ToList();

			//assert
			CollectionAssert.AreEqual(testContext.SingleUser.PhoneBook.Where(x => x.Email.Contains("treyes0@")).ToList(), retContacts);

			contactService.Dispose();
		}

		[TestMethod]
		public void SearchContactsByNameOnContactService()
		{
			//arrange
			ContactService contactService = new ContactService(MockUnitOfWork.Object);

			//act
			List<Contact> retContacts = contactService.SearchContactsByName(testContext.SingleUser.Id, "S", "Tucker").ToList();

			//assert
			CollectionAssert.AreEqual(testContext.SingleUser.PhoneBook.Where(x => x.Forename.Contains("S") && x.Surname.Contains("Tucker")).ToList(), retContacts);

			contactService.Dispose();
		}

		[TestMethod]
		public void SearchContactsByNameAndEmailOnContactService()
		{
			//arrange
			ContactService contactService = new ContactService(MockUnitOfWork.Object);

			//act
			List<Contact> retContacts = contactService.Search(testContext.SingleUser.Id, "Tuc", "tuttocitta").ToList();

			//assert
			CollectionAssert.AreEqual(testContext.SingleUser.PhoneBook.Where(x => (x.Forename + " " + x.Surname).Contains("Tucker") && x.Email.Contains("tuttocitta")).ToList(), retContacts);

			contactService.Dispose();
		}
		#endregion
	}
}
