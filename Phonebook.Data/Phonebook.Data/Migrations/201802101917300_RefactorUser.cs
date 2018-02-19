namespace Phonebook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Token", "UserId", "dbo.User");
            DropIndex("dbo.Token", new[] { "UserId" });
            DropColumn("dbo.User", "Password");
            DropTable("dbo.Token");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Token", "UserId");
            AddForeignKey("dbo.Token", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
