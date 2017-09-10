using Phonebook.Domain.Model;
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
            builder.Namespace = "Phonebook";
            builder.EntitySet<Contact>("Contacts");
            builder.EntitySet<User>("Users");

            var function = builder.EntityType<User>().Function("MyContacts").ReturnsCollectionFromEntitySet<Contact>("Contacts");

            var function2 = builder.Function("Authenticate");
            function2.Parameter<string>("username");
            function2.Parameter<string>("password");
            function2.ReturnsFromEntitySet<User>("User");

            var function3 = builder.EntityType<Contact>().Function("GetContactNumbers").ReturnsCollectionFromEntitySet<ContactNumber>("ContactNumbers");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel()
                );
        }
    }
}
