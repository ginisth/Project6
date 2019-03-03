namespace Project6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentFieldsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Documents", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Documents", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "Content", c => c.String());
            AlterColumn("dbo.Documents", "Title", c => c.String());
        }
    }
}
