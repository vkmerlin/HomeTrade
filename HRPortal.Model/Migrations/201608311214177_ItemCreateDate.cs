namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemCreateDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradeItems", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TradeItems", "CreateDate");
        }
    }
}
