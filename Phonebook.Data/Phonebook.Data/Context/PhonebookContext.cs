using Phonebook.Domain.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Phonebook.Data.Context
{
	public class PhonebookContext : DbContext
    {
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Contact> Contacts { get; set; }
		public virtual DbSet<ContactNumber> ContactNumbers { get; set; }
		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}
