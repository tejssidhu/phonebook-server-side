using System.ComponentModel.DataAnnotations;

namespace Phonebook.WebApi.Model
{
	public class Ping
	{
		[Key]
		public string Response { get; set; }
	}
}