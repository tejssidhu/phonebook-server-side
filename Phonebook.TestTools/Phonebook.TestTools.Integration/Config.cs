using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.TestTools.Integration
{
    public static class Config
    {
		public static string BaseUrl;

		static Config()
		{
			BaseUrl = ConfigurationManager.AppSettings.Get("baseUrl");
		}
    }
}
