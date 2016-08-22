namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Items : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TradeItemAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TradeItemId = c.Int(nullable: false),
                        AttachementPath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TradeItems", t => t.TradeItemId, cascadeDelete: true)
                .Index(t => t.TradeItemId);
            
            CreateTable(
                "dbo.TradeItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TradeItems", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.TradeItemAttachments", "TradeItemId", "dbo.TradeItems");
            DropIndex("dbo.TradeItems", new[] { "CategoryId" });
            DropIndex("dbo.TradeItemAttachments", new[] { "TradeItemId" });
            DropTable("dbo.TradeItems");
            DropTable("dbo.TradeItemAttachments");
            DropTable("dbo.Categories");
        }
    }
}
