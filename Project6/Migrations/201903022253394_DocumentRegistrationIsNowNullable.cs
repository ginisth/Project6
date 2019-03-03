namespace Project6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentRegistrationIsNowNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Documents", "DocumentRegistration", c => c.Boolean());
            Sql("DELETE FROM Users");
            Sql("INSERT INTO Users(Username,Password,UserRole,UserRegistration) VALUES('Admin','Admin',5,1)");
            Sql("DELETE FROM Documents");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "DocumentRegistration", c => c.Boolean(nullable: false));
        }
    }
}
