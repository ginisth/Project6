namespace Project6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAnnotationsInUsers : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM Users");
            Sql("INSERT INTO Users(Username,Password,UserRole,UserRegistration) VALUES('God','God',5,1)");

            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
        }
    }
}
