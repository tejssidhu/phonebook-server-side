using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phonebook.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Phonebook.Domain.Exceptions;

namespace Phonebook.Data.Tests
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class ContatNumberRepositoryTests : BaseRespositoryTests
	{
		[TestMethod]
		public void GetAllOnContactNumberRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactNumbersList = new List<ContactNumber>(_contactNumbers.OrderBy(c => c.Description).ThenBy(n => n.TelephoneNumber));

			//Act
			List<ContactNumber> contactNumbers = unitOfWork.ContactNumberRepository.GetAll().ToList();

			List<ContactNumber> unProxiedContactNumbers = new List<ContactNumber>();
			foreach (var cn in contactNumbers)
			{
				unProxiedContactNumbers.Add(UnProxy(cn));
			}
			unProxiedContactNumbers = unProxiedContactNumbers.OrderBy(c => c.Description).ThenBy(n => n.TelephoneNumber).ToList();

			//Assert
			CollectionAssert.AreEqual(contactNumbersList, unProxiedContactNumbers);

			unitOfWork.Dispose();
		}

		[TestMethod]
		public void GetOnContactNumberRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactNumbersList = new List<ContactNumber>(_contactNumbers);

			//Act
			ContactNumber contactNumber = unitOfWork.ContactNumberRepository.Get(new Guid("9a005b3e-d9ec-4e08-aefa-589ab5e00bfa"));

			//Assert
			Assert.AreEqual(contactNumbersList[0], UnProxy(contactNumber));

			unitOfWork.Dispose();
		}

		[TestMethod]
		public void CreateOnContactNumberRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);

			var contactNumberToCreate = new ContactNumber
			{
				ContactId = new Guid("cc772bf2-40bd-4b25-9e3a-0e80b1a63383"),
				Description = "Mobile",
				TelephoneNumber = "0123456789"
			};

			//Act
			unitOfWork.ContactNumberRepository.Create(contactNumberToCreate);
			unitOfWork.SaveChanges();
			ContactNumber contactNumber = unitOfWork.ContactNumberRepository.Get(contactNumberToCreate.Id);

			//Assert
			Assert.AreEqual(UnProxy(contactNumber), contactNumberToCreate);
			Assert.IsNotNull(contactNumberToCreate.Id);
			
			unitOfWork.Dispose();
		}

		[TestMethod]
		public void UpdateOnContactNumberRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactNumbersList = new List<ContactNumber>(_contactNumbers);

			var contactNumberToUpdate = contactNumbersList[3];
			contactNumberToUpdate.Description = "Mobile" + "Updated";
			contactNumberToUpdate.TelephoneNumber = "0123456789" + "Updated";

			//Act
			unitOfWork.ContactNumberRepository.Update(contactNumberToUpdate);
			unitOfWork.SaveChanges();
			ContactNumber contactNumber = unitOfWork.ContactNumberRepository.Get(contactNumberToUpdate.Id);
			
			//Assert
			Assert.AreEqual(UnProxy(contactNumber), contactNumberToUpdate);

			unitOfWork.Dispose();
		}

		[TestMethod]
		[ExpectedException(typeof(ObjectNotFoundException))]
		public void DeleteOnContactNumberRepositoryNotExisting()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);

			//Act
			unitOfWork.ContactNumberRepository.Delete(new Guid());
			
			//Assert -- expected exception
		}

		[TestMethod]
		public void DeleteOnContactNumberRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactNumbersList = new List<ContactNumber>(_contactNumbers);
			var contactNumberToDelete = contactNumbersList[3];

			//Act
			unitOfWork.ContactNumberRepository.Delete(contactNumberToDelete.Id);
			unitOfWork.SaveChanges();
			ContactNumber contactNumber = unitOfWork.ContactNumberRepository.Get(contactNumberToDelete.Id);

			//Assert
			Assert.IsNull(contactNumber);
		}

		[TestMethod]
		public void DeleteByContactIdOnContactNumberRepository()
		{
			//Arrange
			UnitOfWork unitOfWork = new UnitOfWork(phonebookContext);
			var contactNumbersList = new List<ContactNumber>(_contactNumbers);
			var contactNumberToDelete = contactNumbersList[3];

			//Act
			unitOfWork.ContactNumberRepository.DeleteContactNumbersByContactId(contactNumberToDelete.ContactId);
			unitOfWork.SaveChanges();
			int numOfContactNumbers = unitOfWork.ContactRepository.Get(contactNumberToDelete.ContactId).ContactNumbers.Count;

			//Assert
			Assert.AreEqual(0, numOfContactNumbers);
		}

		private ContactNumber UnProxy(dynamic proxiedType)
		{
			ContactNumber contactNumber = new ContactNumber();

			contactNumber.Id = proxiedType.Id;
			contactNumber.ContactId = proxiedType.ContactId;
			contactNumber.Description = proxiedType.Description;
			contactNumber.TelephoneNumber = proxiedType.TelephoneNumber;

			return contactNumber;
		}
	}
}
