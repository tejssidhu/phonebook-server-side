using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;

namespace Phonebook.WebApi.Routing
{
	public class MatchRoutingConventionService : IODataRoutingConvention
	{
		public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
		{
			return null;
		}
		
		public string SelectController(ODataPath odataPath, HttpRequestMessage request)
		{
			return null;
		}
	}
}