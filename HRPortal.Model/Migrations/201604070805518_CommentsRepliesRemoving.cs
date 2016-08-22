namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsRepliesRemoving : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentsReplies", "CommentsReplyId", "dbo.CommentsReplies");
            DropIndex("dbo.CommentsReplies", new[] { "CommentsReplyId" });
            DropTable("dbo.CommentsReplies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CommentsReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsCommentId = c.Int(),
                        CommentsReplyId = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.CommentsReplies", "CommentsReplyId");
            AddForeignKey("dbo.CommentsReplies", "CommentsReplyId", "dbo.CommentsReplies", "Id");
        }
    }
}
