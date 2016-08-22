namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Price : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradeItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TradeItems", "Price");
        }
    }
}
