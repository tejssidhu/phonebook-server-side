using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using Phonebook.WebApi.App_Start;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Phonebook.WebApi.Startup))]
namespace Phonebook.WebApi
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();

			WebApiConfig.Register(config);
			StructureMapConfig.RegisterResolver(config);

			//TODO: make this more secure i.e. http://localhost:4200
			var cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			app.UseIdentityServerBearerTokenAuthentication(new IdentityServer3.AccessTokenValidation.IdentityServerBearerTokenAuthenticationOptions
			{
				AuthenticationMode = AuthenticationMode.Active,
				Authority = "https://localhost:44301",
				RequiredScopes = new[] { "phonebookAPI.read", "phonebookAPI.write" }
			});

			app.UseWebApi(config);

			app.Run(context =>
			{
				context.Response.ContentType = "text/plain";
				return context.Response.WriteAsync("Hello, world.");
			});
		}
	}
}