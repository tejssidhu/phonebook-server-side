namespace Phonebook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedToken : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        AuthToken = c.String(nullable: false, maxLength: 250),
                        IssuedOn = c.DateTime(nullable: false),
                        ExpiresOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Token", "UserId", "dbo.User");
            DropIndex("dbo.Token", new[] { "UserId" });
            DropTable("dbo.Token");
        }
    }
}
