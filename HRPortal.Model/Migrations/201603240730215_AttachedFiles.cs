namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttachedFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsAttachedFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsId = c.Int(nullable: false),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsAttachedFiles", "NewsId", "dbo.News");
            DropIndex("dbo.NewsAttachedFiles", new[] { "NewsId" });
            DropTable("dbo.NewsAttachedFiles");
        }
    }
}
