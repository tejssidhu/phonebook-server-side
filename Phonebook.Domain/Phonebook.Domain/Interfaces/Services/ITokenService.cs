using Phonebook.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Interfaces.Services
{
	public interface ITokenService : IDisposable
	{
		Token GenerateToken(Guid userId);
		bool ValidateToken(string tokenId);
		bool Kill(string tokenId);
		bool DeleteByUserId(Guid userId);
	}
}
