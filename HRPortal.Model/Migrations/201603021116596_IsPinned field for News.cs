namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsPinnedfieldforNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "IsPin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "IsPin");
        }
    }
}
