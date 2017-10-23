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
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // one to many config
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithRequired(p => p.Category)
                .WillCascadeOnDelete(true);

            // many to many config
            modelBuilder.Entity<Product>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Products);
        }
    }
}
