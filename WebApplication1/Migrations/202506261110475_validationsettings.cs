namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationsettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IPAddress", c => c.String());
            AlterColumn("dbo.Passwords", "Description", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Passwords", "URL", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Passwords", "Username", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Passwords", "PasswordField", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "PasswordHash", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "ResetToken", c => c.String(maxLength: 88));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "ResetToken", c => c.String());
            AlterColumn("dbo.Users", "PasswordHash", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Passwords", "PasswordField", c => c.String());
            AlterColumn("dbo.Passwords", "Username", c => c.String());
            AlterColumn("dbo.Passwords", "URL", c => c.String());
            AlterColumn("dbo.Passwords", "Description", c => c.String());
            DropColumn("dbo.Users", "IPAddress");
        }
    }
}
