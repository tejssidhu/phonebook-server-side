using Phonebook.WebApi.DependencyResolution;
using System.Web.Http;

namespace Phonebook.WebApi.App_Start
{
	public class StructureMapConfig
	{
		public static void RegisterResolver(HttpConfiguration config)
		{
			var container = IoC.Initialize();
			config.DependencyResolver = new StructureMapResolver(container);
		}
	}
}