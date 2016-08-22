namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsReplies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsCommentsId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsComments", t => t.NewsCommentsId, cascadeDelete: true)
                .Index(t => t.NewsCommentsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsReplies", "NewsCommentsId", "dbo.NewsComments");
            DropIndex("dbo.NewsReplies", new[] { "NewsCommentsId" });
            DropTable("dbo.NewsReplies");
        }
    }
}
