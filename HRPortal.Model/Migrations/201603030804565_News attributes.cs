namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newsattributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.News", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Message", c => c.String());
            AlterColumn("dbo.News", "Title", c => c.String());
        }
    }
}
