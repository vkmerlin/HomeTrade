namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsRepliesRequiredfields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NewsReplies", "NewsCommentsId", "dbo.NewsComments");
            DropIndex("dbo.NewsReplies", new[] { "NewsCommentsId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.NewsReplies", "NewsCommentsId");
            AddForeignKey("dbo.NewsReplies", "NewsCommentsId", "dbo.NewsComments", "Id", cascadeDelete: true);
        }
    }
}
