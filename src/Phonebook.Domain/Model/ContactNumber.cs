using Phonebook.Domain.Interfaces.Model;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Phonebook.Domain.Model
{
	[DataContract]
	public class ContactNumber : IEntity
	{
		[Required]
		[DataMember(Name = "id")]
		public Guid Id { get; set; }
		
		[Required]
		[DataMember(Name = "contactId")]
		public Guid ContactId { get; set; }

		[Required]
		[MaxLength(20)]
		[DataMember(Name = "description")]
		public string Description { get; set; }

		[Required]
		[MaxLength(20)]
		[DisplayName("Telephone Number")]
		[DataMember(Name = "telephoneNumber")]
		public string TelephoneNumber { get; set; }

		//defined as virtual so that they can take advantage of certain Entity Framework functionality such as lazy loading
		public virtual Contact Contact { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			ContactNumber cn = (ContactNumber)obj;

			return (ContactId == cn.ContactId) && (Description == cn.Description) && (TelephoneNumber == cn.TelephoneNumber);
		}

		protected bool Equals(ContactNumber other)
		{
			return ContactId.Equals(other.ContactId) && string.Equals(Description, other.Description) && string.Equals(TelephoneNumber, other.TelephoneNumber);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = ContactId.GetHashCode();
				hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ (TelephoneNumber != null ? TelephoneNumber.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}