using Phonebook.Domain.Exceptions;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Interfaces.UnitOfWork;
using Phonebook.Domain.Model;
using System;
using System.Linq;

namespace Phonebook.Domain.Services
{
	public class SampleContactService : ISampleContactService
	{
		private readonly IUnitOfWork _unitOfWork;

		public SampleContactService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public Guid Create(SampleContact model)
		{
			var entity = _unitOfWork.SampleContactRepository.GetAll(u => u.Email == model.Email).SingleOrDefault();

			if (entity != null) throw new ObjectAlreadyExistException("SampleContact");

			_unitOfWork.SampleContactRepository.Create(model);
			_unitOfWork.SaveChanges();

			return model.Id;
		}

		public void Delete(Guid id)
		{
			_unitOfWork.SampleContactRepository.Delete(id);

			_unitOfWork.SaveChanges();
		}

		public void Dispose()
		{
			_unitOfWork.Dispose();
		}

		public IQueryable<SampleContact> Get(Guid id)
		{
			return _unitOfWork.SampleContactRepository.Get(id);
		}

		public IQueryable<SampleContact> GetAll()
		{
			return _unitOfWork.SampleContactRepository.GetAll();
		}

		public void Update(SampleContact model)
		{
			if (_unitOfWork.SampleContactRepository.GetAll(u => u.Email == model.Email && u.Id != model.Id).Any())
			{
				throw new ObjectAlreadyExistException("SampleContact", "email");
			}

			_unitOfWork.SampleContactRepository.Update(model);

			_unitOfWork.SaveChanges();
		}
	}
}
