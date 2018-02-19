using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace Phonebook.Data.Migrations
{
	[ExcludeFromCodeCoverage]
	public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactNumber",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContactId = c.Guid(nullable: false),
                        Description = c.String(nullable: false, maxLength: 20),
                        TelephoneNumber = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Title = c.String(maxLength: 100),
                        Forename = c.String(maxLength: 100),
                        Surname = c.String(maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "UserId", "dbo.User");
            DropForeignKey("dbo.ContactNumber", "ContactId", "dbo.Contact");
            DropIndex("dbo.Contact", new[] { "UserId" });
            DropIndex("dbo.ContactNumber", new[] { "ContactId" });
            DropTable("dbo.User");
            DropTable("dbo.Contact");
            DropTable("dbo.ContactNumber");
        }
    }
}
