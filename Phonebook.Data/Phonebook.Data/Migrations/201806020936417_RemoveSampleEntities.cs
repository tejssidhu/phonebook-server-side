namespace Phonebook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSampleEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SampleUser", "AddressId", "dbo.SampleAddress");
            DropForeignKey("dbo.SampleContact", "UserId", "dbo.SampleUser");
            DropIndex("dbo.SampleContact", new[] { "UserId" });
            DropIndex("dbo.SampleUser", new[] { "AddressId" });
            DropTable("dbo.SampleAddress");
            DropTable("dbo.SampleContact");
            DropTable("dbo.SampleUser");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
            
            CreateIndex("dbo.SampleUser", "AddressId");
            CreateIndex("dbo.SampleContact", "UserId");
            AddForeignKey("dbo.SampleContact", "UserId", "dbo.SampleUser", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SampleUser", "AddressId", "dbo.SampleAddress", "Id", cascadeDelete: true);
        }
    }
}
