using System.Data.Entity;
using WebTemplate.Database.Configurations;
using WebTemplate.Models;

namespace WebTemplate.Database
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<TestModel> TestModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Moved all Student related configuration to StudentEntityConfiguration class
            modelBuilder.Configurations.Add(new TestModelConfiguration());
        }
    }
}
