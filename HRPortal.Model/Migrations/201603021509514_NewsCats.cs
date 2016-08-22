namespace HRPortal.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsCats : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.News", "NewCategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.News", "NewsCategory_Id", c => c.Int());
            CreateIndex("dbo.News", "NewsCategory_Id");
            AddForeignKey("dbo.News", "NewsCategory_Id", "dbo.NewsCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsCategory_Id", "dbo.NewsCategories");
            DropIndex("dbo.News", new[] { "NewsCategory_Id" });
            DropColumn("dbo.News", "NewsCategory_Id");
            DropColumn("dbo.News", "NewCategoryId");
            DropTable("dbo.NewsCategories");
        }
    }
}
