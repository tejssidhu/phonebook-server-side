using Phonebook.TestTools.Data;
using System;

namespace Tester
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(DataManager.IdentityServerConnString);
			Console.WriteLine(DataManager.PhonebookConnString);
			Guid userId = Guid.Empty;

			try
			{
				userId = IdentityManager.CreateNewUserWithPassword1AndAllClaims("TestUser1");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.WriteLine("user created: " + userId.ToString());
			Console.ReadLine();

			IdentityManager.DeleteUser(userId);
			Console.ReadLine();
		}
	}
}
