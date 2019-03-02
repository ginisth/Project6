namespace Project6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDatabaseWithaManager : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Users(Username,Password,UserRole,UserRegistration) VALUES('Manager','Manager',5,1)");
        }
        
        public override void Down()
        {
        }
    }
}
