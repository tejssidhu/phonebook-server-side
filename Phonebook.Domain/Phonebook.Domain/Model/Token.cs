using Phonebook.Domain.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Model
{
	public class Token : IEntity
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public Guid UserId { get; set; }
		
		[Required]
		[MaxLength(250)]
		public string AuthToken { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[Display(Name="Issued On")]
		public DateTime IssuedOn { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[Display(Name = "Expires On")]
		public DateTime ExpiresOn { get; set; }
		
		//defined as virtual so that they can take advantage of certain Entity Framework functionality such as lazy loading
		public virtual User User { get; set; }
	}
}
