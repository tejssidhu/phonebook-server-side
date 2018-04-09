using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phonebook.WebApi.Integration.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestInitialize]
		public void TestInitialise()
		{

		}

		[TestMethod]
		public async Task GetContacts()
		{
			var httpclient = new HttpClient();
			try
			{
				var response = await httpclient.GetAsync("http://localhost/Phonebook.WebApi/Users");
			} catch (Exception e)
			{

			}
		}
	}
}
