using Phonebook.Domain.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Phonebook.Domain.Model
{
	[DataContract]
	public class SampleUser : IEntity
	{
		[Required]
		[DataMember(Name = "id")]
		public Guid Id { get; set; }

		[ForeignKey("SampleAddress")]
		[Required]
		[DataMember(Name = "addressId")]
		public Guid AddressId { get; set; }

		[DataMember(Name = "forename")]
		public string Forename { get; set; }

		[DataMember(Name = "surname")]
		public string Surname { get; set; }

		[DataMember(Name = "email")]
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		[MaxLength(100)]
		public string Email { get; set; }

		[DataMember(Name = "web")]
		public string Web { get; set; }

		[DataMember(Name = "dob")]
		public DateTime DateOfBirth { get; set; }

		[DataMember(Name = "canDrive")]
		public Boolean CanDrive { get; set; }

		[DataMember(Name = "annualSalary")]
		public Decimal AnnualSalary { get; set; }

		[DataMember(Name = "pastRoles")]
		public int PastRoles { get; set; }

		public virtual SampleAddress SampleAddress { get; set; }
		public virtual ICollection<SampleContact> SampleContacts { get; set; }

		public SampleUser()
		{
			if (SampleContacts == null)
				SampleContacts = new List<SampleContact>();
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			SampleUser c = (SampleUser)obj;

			return (AddressId == c.AddressId) && (Forename == c.Forename) && (Surname == c.Surname) && (Email == c.Email) && (Web == c.Web) && (DateOfBirth == c.DateOfBirth) && (CanDrive == c.CanDrive) && (AnnualSalary == c.AnnualSalary) && (PastRoles == c.PastRoles);
		}

		protected bool Equals(SampleUser other)
		{
			return Guid.Equals(AddressId, other.AddressId) && string.Equals(Forename, other.Forename) && string.Equals(Surname, other.Surname) && string.Equals(Email, other.Email) && string.Equals(Web, other.Web) && DateTime.Equals(DateOfBirth, other.DateOfBirth) && Boolean.Equals(CanDrive, other.CanDrive) && Decimal.Equals(AnnualSalary, other.AnnualSalary) && int.Equals(PastRoles, other.PastRoles);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = AddressId.GetHashCode();
				hashCode = (hashCode * 397) ^ (Forename != null ? Forename.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Surname != null ? Surname.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Email != null ? Email.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Web != null ? Web.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (DateOfBirth != null ? DateOfBirth.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (CanDrive.GetHashCode());
				hashCode = (hashCode * 397) ^ (AnnualSalary.GetHashCode());
				hashCode = (hashCode * 397) ^ (PastRoles.GetHashCode());
				return hashCode;
			}
		}
	}
}
