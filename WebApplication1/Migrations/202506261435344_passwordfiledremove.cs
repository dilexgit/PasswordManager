namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passwordfiledremove : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Passwords", "EncryptedPassword", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Passwords", "PasswordField");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passwords", "PasswordField", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Passwords", "EncryptedPassword", c => c.String());
        }
    }
}
