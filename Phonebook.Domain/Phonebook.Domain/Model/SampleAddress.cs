using Phonebook.Domain.Interfaces.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Phonebook.Domain.Model
{
	[DataContract]
	public class SampleAddress : IEntity
	{
		[Required]
		[DataMember(Name = "id")]
		public Guid Id { get; set; }

		[DataMember(Name = "address")]
		public string Address { get; set; }

		[DataMember(Name = "city")]
		public string City { get; set; }

		[DataMember(Name = "county")]
		public string County { get; set; }

		[DataMember(Name = "postcode")]
		public string Postcode { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			SampleAddress c = (SampleAddress)obj;

			return (Address == c.Address) && (City== c.City) && (County == c.County) && (Postcode == c.Postcode);
		}

		protected bool Equals(SampleAddress other)
		{
			return string.Equals(Address, other.Address) && string.Equals(City, other.City) && string.Equals(County, other.County) && string.Equals(Postcode, other.Postcode);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Address != null ? Address.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (City != null ? City.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (County != null ? County.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Postcode != null ? Postcode.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
