﻿using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Interfaces.UnitOfWork;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Domain.Services
{
	public class ContactService : IContactService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ContactService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        #region Get methods
        public IQueryable<Contact> GetAll()
		{
			return _unitOfWork.ContactRepository.GetAll();
		}

		public IQueryable<Contact> Get(Guid id)
		{
			return _unitOfWork.ContactRepository.Get(id);
		}
		#endregion

		#region Create, update and delete method
		public Guid Create(Contact model)
		{
			var user = _unitOfWork.UserRepository.Get(model.UserId).FirstOrDefault();

			if (user == null) throw new ObjectNotFoundException("User");

			var contact = _unitOfWork.ContactRepository.GetAll(c => c.UserId == model.UserId && c.Email == model.Email).SingleOrDefault();

			if (contact != null) throw new ObjectAlreadyExistException("Contact", "Email");

			_unitOfWork.ContactRepository.Create(model);
			_unitOfWork.SaveChanges();

			return model.Id;
		}

		public void Update(Contact model)
		{
			if (_unitOfWork.ContactRepository.GetAll(c => c.UserId == model.UserId && c.Id != model.Id && c.Email == model.Email).Any())
			{
				throw new ObjectAlreadyExistException("Contact", "Email");
			}

			_unitOfWork.ContactRepository.Update(model);

			_unitOfWork.SaveChanges();
		}

		public void Delete(Guid id)
		{
			_unitOfWork.ContactRepository.Delete(id);

			_unitOfWork.SaveChanges();
		}
        #endregion
	}
}
