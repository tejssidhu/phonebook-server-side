using System.Configuration;

namespace Phonebook.TestTools.Data
{
	public static class DataManager
	{
		public static string IdentityServerConnString;
		public static string PhonebookConnString;

		static DataManager()
		{
			IdentityServerConnString = ConfigurationManager.AppSettings.Get("identityServerConnString");
			PhonebookConnString = ConfigurationManager.AppSettings.Get("phonebookConnString");
		}


	}
}