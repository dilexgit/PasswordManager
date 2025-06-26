namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PasswordAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passwords", "PasswordField", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passwords", "PasswordField");
        }
    }
}
