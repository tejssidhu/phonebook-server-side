namespace Phonebook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSampleObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SampleAddress",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        County = c.String(),
                        Postcode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SampleContact",
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
                .ForeignKey("dbo.SampleUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SampleUser",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        Forename = c.String(),
                        Surname = c.String(),
                        Email = c.String(maxLength: 100),
                        Web = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        CanDrive = c.Boolean(nullable: false),
                        AnnualSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PastRoles = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SampleAddress", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SampleContact", "UserId", "dbo.SampleUser");
            DropForeignKey("dbo.SampleUser", "AddressId", "dbo.SampleAddress");
            DropIndex("dbo.SampleUser", new[] { "AddressId" });
            DropIndex("dbo.SampleContact", new[] { "UserId" });
            DropTable("dbo.SampleUser");
            DropTable("dbo.SampleContact");
            DropTable("dbo.SampleAddress");
        }
    }
}
