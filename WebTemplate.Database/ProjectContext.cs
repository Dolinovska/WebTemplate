using System.Data.Entity;

namespace WebTemplate.Database
{
    using WebTemplate.Database.Models;

    public class WebTemplateContext : DbContext
    {
        public WebTemplateContext()
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // one to many config
            modelBuilder.Entity<Category>()
                .HasMany(c => c.News)
                .WithRequired(p => p.Category)
                .WillCascadeOnDelete(true);
        }
    }
}
