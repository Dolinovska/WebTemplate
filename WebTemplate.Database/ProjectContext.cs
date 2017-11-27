using System.Data.Entity;
using WebTemplate.Database.Models;

namespace WebTemplate.Database
{
    public class WebTemplateContext : DbContext
    {
        public WebTemplateContext()
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> News { get; set; }

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
