using Phonebook.Domain.Model;
using System;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace Phonebook.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {   
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Web API configuration and services
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Contact>("Contacts");

            var function = builder.Function("GetByUser");
            function.Parameter<Guid>("UserId");
            function.ReturnsCollectionFromEntitySet<Contact>("UsersContacts");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}
