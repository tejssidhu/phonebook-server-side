using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Interfaces.UnitOfWork;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Domain.Services
{
    public class ContactNumberService : IContactNumberService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ContactNumberService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        #region Get methods
        public IEnumerable<ContactNumber> GetAll()
		{
			return _unitOfWork.ContactNumberRepository.GetAll();
		}

		public ContactNumber Get(Guid id)
		{
			return _unitOfWork.ContactNumberRepository.Get(id);
		}
        #endregion

        #region Create, update and delete method
        public Guid Create(ContactNumber model)
		{
			var contact = _unitOfWork.ContactRepository.Get(model.ContactId);

			if (contact == null) throw new ObjectNotFoundException("Contact");

			var contactNumber = _unitOfWork.ContactNumberRepository.GetAll(c => c.ContactId == model.ContactId && c.TelephoneNumber == model.TelephoneNumber).SingleOrDefault();

			if (contactNumber != null) throw new ObjectAlreadyExistException("Contact Number", "telephone number");

			_unitOfWork.ContactNumberRepository.Create(model);
			_unitOfWork.SaveChanges();

			return model.Id;
		}

		public void Update(ContactNumber model)
		{
			if (_unitOfWork.ContactNumberRepository.GetAll(c => c.ContactId == model.ContactId && c.Id != model.Id && c.TelephoneNumber == model.TelephoneNumber).Any())
			{
				throw new ObjectAlreadyExistException("Contact Number", "telephone number");
			}

			_unitOfWork.ContactNumberRepository.Update(model);

			_unitOfWork.SaveChanges();
		}
		
		public void Delete(Guid id)
		{
			_unitOfWork.ContactNumberRepository.Delete(id);

			_unitOfWork.SaveChanges();
		}
        #endregion
	}
}
