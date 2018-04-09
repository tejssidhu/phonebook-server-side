using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Interfaces.UnitOfWork;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Services
{
	public class SampleUserService : ISampleUserService
	{
		private readonly IUnitOfWork _unitOfWork;

		public SampleUserService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public Guid Create(SampleUser model)
		{
			var entity = _unitOfWork.SampleUserRepository.GetAll(u => u.Email == model.Email).SingleOrDefault();

			if (entity != null) throw new ObjectAlreadyExistException("SampleUser");

			_unitOfWork.SampleUserRepository.Create(model);
			_unitOfWork.SaveChanges();

			return model.Id;
		}

		public void Delete(Guid id)
		{
			_unitOfWork.SampleUserRepository.Delete(id);

			_unitOfWork.SaveChanges();
		}

		public void Dispose()
		{
			_unitOfWork.Dispose();
		}

		public IQueryable<SampleUser> Get(Guid id)
		{
			return _unitOfWork.SampleUserRepository.Get(id);
		}

		public IQueryable<SampleUser> GetAll()
		{
			return _unitOfWork.SampleUserRepository.GetAll();
		}

		public void Update(SampleUser model)
		{
			if (_unitOfWork.SampleUserRepository.GetAll(u => u.Email == model.Email && u.Id != model.Id).Any())
			{
				throw new ObjectAlreadyExistException("SampleUser", "email");
			}

			_unitOfWork.SampleUserRepository.Update(model);

			_unitOfWork.SaveChanges();
		}
	}
}
