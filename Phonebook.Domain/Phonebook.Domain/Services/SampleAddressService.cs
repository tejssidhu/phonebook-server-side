using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Interfaces.UnitOfWork;
using Phonebook.Domain.Model;
using System;
using System.Linq;

namespace Phonebook.Domain.Services
{
	public class SampleAddressService : ISampleAddressService
	{
		private readonly IUnitOfWork _unitOfWork;

		public SampleAddressService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public Guid Create(SampleAddress model)
		{
			_unitOfWork.SampleAddressRepository.Create(model);
			_unitOfWork.SaveChanges();

			return model.Id;
		}

		public void Delete(Guid id)
		{
			_unitOfWork.SampleAddressRepository.Delete(id);

			_unitOfWork.SaveChanges();
		}

		public void Dispose()
		{
			_unitOfWork.Dispose();
		}

		public IQueryable<SampleAddress> Get(Guid id)
		{
			return _unitOfWork.SampleAddressRepository.Get(id);
		}

		public IQueryable<SampleAddress> GetAll()
		{
			return _unitOfWork.SampleAddressRepository.GetAll();
		}

		public void Update(SampleAddress model)
		{
			_unitOfWork.SampleAddressRepository.Update(model);

			_unitOfWork.SaveChanges();
		}
	}
}
