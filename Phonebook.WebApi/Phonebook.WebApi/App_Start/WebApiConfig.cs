using Phonebook.Domain.Model;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Phonebook.WebApi.Model;
using Microsoft.OData.Edm;

namespace Phonebook.WebApi.App_Start
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
			builder.EntitySet<ContactNumber>("ContactNumbers");
			builder.EntitySet<Contact>("Contacts");
			builder.EntitySet<User>("Users");
			builder.EntitySet<Ping>("Ping");

			var model = builder.GetEdmModel();
			AddNavigations(model);

			config.MapODataServiceRoute(
				routeName: "ODataRoute",
				routePrefix: null,
				model: model
			);

			config.Select().Expand().Filter().OrderBy().MaxTop(null).Count();
		}

		// https://stackoverflow.com/questions/27568899/getting-related-entities-asp-net-webapi-odata-v4-results-in-no-http-resource-wa
		private static void AddNavigations(IEdmModel model)
		{
			AddContactsNavigation(model);
			AddContactNumbersNavigation(model);
			AddSampleContactsNavigation(model);
		}

		private static void AddContactsNavigation(IEdmModel model)
		{
			var users = (EdmEntitySet)model.EntityContainer.FindEntitySet("Users");
			var contacts = (EdmEntitySet)model.EntityContainer.FindEntitySet("Contacts");
			var userType = (EdmEntityType)model.FindDeclaredType("Phonebook.Domain.Model.User");
			var contactType = (EdmEntityType)model.FindDeclaredType("Phonebook.Domain.Model.Contact");
			AddOneToManyNavigation("Contacts", users, contacts, userType, contactType);
			AddManyToOneNavigation("User", users, contacts, userType, contactType);
		}

		private static void AddSampleContactsNavigation(IEdmModel model)
		{
			var users = (EdmEntitySet)model.EntityContainer.FindEntitySet("SampleUsers");
			var contacts = (EdmEntitySet)model.EntityContainer.FindEntitySet("SampleContacts");
			var userType = (EdmEntityType)model.FindDeclaredType("Phonebook.Domain.Model.SampleUser");
			var contactType = (EdmEntityType)model.FindDeclaredType("Phonebook.Domain.Model.SampleContact");
			AddOneToManyNavigation("SampleContacts", users, contacts, userType, contactType);
			AddManyToOneNavigation("SampleUser", users, contacts, userType, contactType);
		}

		private static void AddContactNumbersNavigation(IEdmModel model)
		{
			var contacts = (EdmEntitySet)model.EntityContainer.FindEntitySet("Contacts");
			var contactNumbers = (EdmEntitySet)model.EntityContainer.FindEntitySet("ContactNumbers");
			var contactType = (EdmEntityType)model.FindDeclaredType("Phonebook.Domain.Model.Contact");
			var contactNumberType = (EdmEntityType)model.FindDeclaredType("Phonebook.Domain.Model.ContactNumber");
			AddOneToManyNavigation("ContactNumbers", contacts, contactNumbers, contactType, contactNumberType);
			AddManyToOneNavigation("Contact", contacts, contactNumbers, contactType, contactNumberType);
		}

		private static void AddOneToManyNavigation(string navTargetName, EdmEntitySet oneEntitySet, EdmEntitySet manyEntitySet, EdmEntityType oneEntityType, EdmEntityType manyEntityType)
		{
			var navPropertyInfo = new EdmNavigationPropertyInfo
			{
				TargetMultiplicity = EdmMultiplicity.Many,
				Target = manyEntityType,
				ContainsTarget = false,
				OnDelete = EdmOnDeleteAction.None,
				Name = navTargetName
			};

			oneEntitySet.AddNavigationTarget(oneEntityType.AddUnidirectionalNavigation(navPropertyInfo), manyEntitySet);
		}

		private static void AddManyToOneNavigation(string navTargetName, EdmEntitySet oneEntitySet, EdmEntitySet manyEntitySet, EdmEntityType oneEntityType, EdmEntityType manyEntityType)
		{
			var navPropertyInfo = new EdmNavigationPropertyInfo
			{
				TargetMultiplicity = EdmMultiplicity.One,
				Target = oneEntityType,
				ContainsTarget = false,
				OnDelete = EdmOnDeleteAction.None,
				Name = navTargetName
			};

			manyEntitySet.AddNavigationTarget(manyEntityType.AddUnidirectionalNavigation(navPropertyInfo), oneEntitySet);
		}
	}
}