namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsReplies : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommentsReplies", t => t.CommentsReplyId)
                .Index(t => t.CommentsReplyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentsReplies", "CommentsReplyId", "dbo.CommentsReplies");
            DropIndex("dbo.CommentsReplies", new[] { "CommentsReplyId" });
            DropTable("dbo.CommentsReplies");
        }
    }
}
