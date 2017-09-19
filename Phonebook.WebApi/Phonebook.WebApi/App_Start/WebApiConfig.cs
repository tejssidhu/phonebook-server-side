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

            var action = builder.Action("Authenticate");
            action.Parameter<string>("username");
            action.Parameter<string>("password");
            action.ReturnsFromEntitySet<User>("User");

            var function3 = builder.EntityType<Contact>().Function("GetContactNumbers").ReturnsCollectionFromEntitySet<ContactNumber>("ContactNumbers");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel()
                );
        }
    }
}
