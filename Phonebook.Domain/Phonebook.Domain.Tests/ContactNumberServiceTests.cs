using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Model;
using Phonebook.Domain.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Phonebook.Domain.Tests
{
	[TestClass]
	public class ContactNumberServiceTests : BaseDomainTests
	{
		#region Test Initialise and Cleanup
		#endregion

		#region tests
		[TestMethod]
		[ExpectedException(typeof(ObjectAlreadyExistException))]
		public void CreateContactNumberForContactWithTelephoneNumberOfExistingContactNumberOnContactNumberService()
		{
			//arrange
			ContactNumber contactNumberToCreate = new ContactNumber
			{
				ContactId = new Guid("81c4763c-b225-4756-903a-750064167813"),
				Description = "Work",
				TelephoneNumber = "297724563901"
			};

			ContactNumberService contactNumberService = new ContactNumberService(mockUnitOfWork.Object);

			//act
			contactNumberService.Create(contactNumberToCreate);

			//assert - expect excpetion

			contactNumberService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectNotFoundException))]
		public void CreateNewContactNumberForNoneExistentContactOnContactNumberService()
		{
			//arrange
			var contactNumberToCreate = new ContactNumber
			{
				ContactId = new Guid("720d966c-5685-45c2-b63d-1312620e1d11"),
				Description = "Work",
				TelephoneNumber = "297724563901"
			};

			ContactNumberService contactNumberService = new ContactNumberService(mockUnitOfWork.Object);

			//act
			Guid contactNumberId = contactNumberService.Create(contactNumberToCreate);

			//assert

			contactNumberService.Dispose();
		}

		[TestMethod]
		public void CreateNewContactOnContactNumberService()
		{
			//arrange
			var contactNumberToCreate = new ContactNumber { ContactId = new Guid("81c4763c-b225-4756-903a-750064167813"), Description = "Work", TelephoneNumber = "201803896534" };

			ContactNumberService contactNumberService = new ContactNumberService(mockUnitOfWork.Object);

			//act
			Guid contactNumberId = contactNumberService.Create(contactNumberToCreate);

			//assert
			mockContactNumberRepository.Verify(y => y.Create(It.IsAny<ContactNumber>()));
			Assert.IsNotNull(contactNumberId);
			Assert.AreEqual(contactNumberId, contactNumberToCreate.Id);

			contactNumberService.Dispose();
		}

		[TestMethod]
		public void GetAllContactNumbersByContactIdOnContactNumberService()
		{
			//arrange
			ContactNumberService contactNumberService = new ContactNumberService(mockUnitOfWork.Object);

			//act
			List<ContactNumber> retContactNumbers = contactNumberService.GetAllByContactId(_contact.Id).ToList();

			//assert
			CollectionAssert.AreEqual(_contact.ContactNumbers.ToList(), retContactNumbers);

			contactNumberService.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectAlreadyExistException))]
		public void UpdateContactNumberToExistingContactNumbersTelephoneNumberOnContactNumberService()
		{
			//arrange
			ContactNumberService contactNumberService = new ContactNumberService(mockUnitOfWork.Object);

			//set email to that of another contact in that users Phonebook
			var contactNumberToUpdate = _contact.ContactNumbers.Where((x, i) => i == 0).FirstOrDefault();
			contactNumberToUpdate.TelephoneNumber = _contact.ContactNumbers.Where((x, i) => i == 1).FirstOrDefault().TelephoneNumber;

			//act
			contactNumberService.Update(contactNumberToUpdate);

			//assert - expected exception

			contactNumberService.Dispose();
		}

		[TestMethod]
		public void UpdateContactNumberOnContactNumberService()
		{
			//arrange
			ContactNumberService contactNumberService = new ContactNumberService(mockUnitOfWork.Object);

			//set email to that of another contact in that users Phonebook
			var contactNumberToUpdate = _contact.ContactNumbers.Where((x, i) => i == 0).FirstOrDefault();
			contactNumberToUpdate.TelephoneNumber = contactNumberToUpdate.TelephoneNumber + "01";

			//act
			contactNumberService.Update(contactNumberToUpdate);

			//assert
			mockContactNumberRepository.Verify(y => y.Update(It.IsAny<ContactNumber>()));

			contactNumberService.Dispose();
		}

		[TestMethod]
		public void DeleteContactNumberOnContactNumberService()
		{
			//arrange
			ContactNumberService contactNumberService = new ContactNumberService(mockUnitOfWork.Object);

			//act
			contactNumberService.Delete(_contactNumber.Id);

			//assert - expected exception
			mockContactNumberRepository.Verify(y => y.Delete(It.IsAny<Guid>()));

			contactNumberService.Dispose();
		}
		#endregion
	}
}
