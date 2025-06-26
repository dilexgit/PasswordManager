namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Passwords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        URL = c.String(),
                        Username = c.String(),
                        EncryptedPassword = c.String(),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ResetToken = c.String(),
                        ResetTokenExpiry = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passwords", "UserId", "dbo.Users");
            DropForeignKey("dbo.Passwords", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Passwords", new[] { "CategoryId" });
            DropIndex("dbo.Passwords", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Passwords");
            DropTable("dbo.Categories");
        }
    }
}
