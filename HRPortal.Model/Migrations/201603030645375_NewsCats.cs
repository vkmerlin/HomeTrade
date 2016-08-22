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
            
            AddColumn("dbo.News", "NewsCategoryId", c => c.Int(nullable: true));
            CreateIndex("dbo.News", "NewsCategoryId");
            AddForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories");
            DropIndex("dbo.News", new[] { "NewsCategoryId" });
            DropColumn("dbo.News", "NewsCategoryId");
            DropTable("dbo.NewsCategories");
        }
    }
}
