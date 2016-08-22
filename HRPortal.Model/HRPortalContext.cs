using HRPortal.Model.Identity;
using HRPortal.Model.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HRPortal.Model
{
    public class HRPortalContext : DbContext
    {
        public HRPortalContext()
            : base("HRPortalContext")
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<NewsCategories> NewsCategories { get; set; }
        public DbSet<NewsAttachedFiles> NewsAttachedFiles { get; set; }
        public DbSet<NewsComments> NewsComments { get; set; }
        public DbSet<NewsReply> NewsReplies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TradeItem> TradeItems { get; set; }
        public DbSet<TradeItemAttachment> TradeItemAttachments { get; set; }

    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("HRPortalContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public static class DataBaseInitializator {
        public static void InitDB()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HRPortalContext, Configuration>());
        }
    }
}
