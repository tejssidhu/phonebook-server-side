using Phonebook.Domain.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Phonebook.Domain.Model
{
    [DataContract]
	public class User : IEntity
	{
		[Required]
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
		
		[Required]
		[MaxLength(100)]
        [DataMember(Name = "username")]
        public string Username { get; set; }
		
		//defined as virtual so that they can take advantage of certain Entity Framework functionality such as lazy loading
		public virtual ICollection<Contact> Contacts { get; set; }

		public User()
		{
			if (Contacts == null)
				Contacts = new List<Contact>();
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			User u = (User)obj;

			return (Username == u.Username);
		}

		protected bool Equals(User other)
		{
			return string.Equals(Username, other.Username);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Username != null ? Username.GetHashCode() : 0)*397);
			}
		}
	}
}
