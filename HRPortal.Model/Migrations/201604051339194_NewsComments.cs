namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsComments", "NewsId", "dbo.News");
            DropIndex("dbo.NewsComments", new[] { "NewsId" });
            DropTable("dbo.NewsComments");
        }
    }
}
