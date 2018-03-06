using System.Web.Http;
using Phonebook.WebApi.Model;

namespace Phonebook.WebApi.Controllers
{
	public class PingController : ApiController
	{
		public IHttpActionResult Get()
		{
			var response = new Ping { Response = "Ok" };

			return Ok(response);
		}
	}
}
