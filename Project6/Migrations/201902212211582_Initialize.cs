namespace Project6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        UserId = c.Int(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        ProgressStatus = c.Int(nullable: false),
                        DocumentRegistration = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        UserRole = c.Int(nullable: false),
                        UserRegistration = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "UserId", "dbo.Users");
            DropIndex("dbo.Documents", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Documents");
        }
    }
}
