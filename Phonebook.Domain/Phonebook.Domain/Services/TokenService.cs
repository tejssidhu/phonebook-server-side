using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Phonebook.Domain.Interfaces.UnitOfWork;

namespace Phonebook.Domain.Services
{
	public class TokenService : ITokenService
	{
		private readonly IUnitOfWork _unitOfWork;
		private double authTokenExpiryInSeconds  {
			get
			{
				return Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]);
			}
		}

		public TokenService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void Dispose()
		{
			_unitOfWork.Dispose();
		}

		public Token GenerateToken(Guid userId)
		{
			string authToken = Guid.NewGuid().ToString();
			DateTime issuedOn = DateTime.Now;
			DateTime expiredOn = DateTime.Now.AddSeconds(authTokenExpiryInSeconds);

			var token = new Token
			{
				Id = Guid.NewGuid(),
				UserId = userId,
				AuthToken = authToken,
				IssuedOn = issuedOn,
				ExpiresOn = expiredOn
			};

			_unitOfWork.TokenRepository.Create(token);
			_unitOfWork.SaveChanges();

			return token;
		}

		public bool ValidateToken(string authToken)
		{
			var token = _unitOfWork.TokenRepository.GetAll(t => t.AuthToken == authToken && t.ExpiresOn > DateTime.Now).SingleOrDefault();

			if (token != null && !(DateTime.Now > token.ExpiresOn))
			{
				token.ExpiresOn = token.ExpiresOn.AddSeconds(authTokenExpiryInSeconds);
				_unitOfWork.TokenRepository.Update(token);
				_unitOfWork.SaveChanges();
				return true;
			}

			return false;
		}

		public bool Kill(string authToken)
		{
			var token  = _unitOfWork.TokenRepository.GetAll(t => t.AuthToken == authToken).SingleOrDefault();

			if (token != null)
			{
				_unitOfWork.TokenRepository.Delete(token);
				_unitOfWork.SaveChanges();
			}

			var isNotDeleted = _unitOfWork.TokenRepository.GetAll(t => t.AuthToken == authToken).Any();
			return !isNotDeleted;
		}

		public bool DeleteByUserId(Guid userId)
		{
			var userTokens = _unitOfWork.TokenRepository.GetAll(t => t.UserId == userId);

			if (userTokens != null)
			{
				foreach (var token in userTokens)
				{
					_unitOfWork.TokenRepository.Delete(token);
				}
				_unitOfWork.SaveChanges();
			}

			var isNotDeleted = _unitOfWork.TokenRepository.GetAll(t => t.UserId == userId).Any();
			return !isNotDeleted;
		}
	}
}
