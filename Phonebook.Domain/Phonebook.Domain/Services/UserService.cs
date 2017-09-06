using System;
using System.Collections.Generic;
using System.Linq;
using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Repositories;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using Phonebook.Domain.Interfaces.UnitOfWork;

namespace Phonebook.Domain.Services
{
    public class UserService : IUserService
    {
		private readonly IUnitOfWork _unitOfWork;

		public UserService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public void Dispose()
        {
			_unitOfWork.Dispose();
        }

        #region Get methods
        public IEnumerable<User> GetAll()
        {
			return _unitOfWork.UserRepository.GetAll();
        }

        public User Get(Guid id)
        {
			return _unitOfWork.UserRepository.Get(id);
        }

        public User Authenticate(string username, string password)
        {
            var user = _unitOfWork.UserRepository.GetAll(u => u.Username == username).SingleOrDefault();

            if (user == null) throw new ObjectNotFoundException("User");

            if (user.Password != password.Trim())
            {
                throw new InvalidPasswordException();
            }

            return user;
        }
        #endregion

        #region Create, Update and delete methods
        public Guid Create(User model)
        {
			var user = _unitOfWork.UserRepository.GetAll(u => u.Username == model.Username).SingleOrDefault();

            if (user != null) throw new ObjectAlreadyExistException("User");

			_unitOfWork.UserRepository.Create(model);
			_unitOfWork.SaveChanges();

			return model.Id;
        }

        public void Update(User model)
        {
			if (_unitOfWork.UserRepository.GetAll(u => u.Username == model.Username && u.Id != model.Id).Any())
            {
                throw new ObjectAlreadyExistException("User", "username");
            }

			_unitOfWork.UserRepository.Update(model);

			_unitOfWork.SaveChanges();
        }

        public void Delete(Guid id)
        {
			_unitOfWork.UserRepository.Delete(id);
			
			_unitOfWork.SaveChanges();
        }
        #endregion
    }
}