namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Navigationproperties : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.NewsReplies", "NewsCommentsId");
            AddForeignKey("dbo.NewsReplies", "NewsCommentsId", "dbo.NewsComments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsReplies", "NewsCommentsId", "dbo.NewsComments");
            DropIndex("dbo.NewsReplies", new[] { "NewsCommentsId" });
        }
    }
}
