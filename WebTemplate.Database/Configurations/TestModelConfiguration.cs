using System.Data.Entity.ModelConfiguration;
using WebTemplate.Models;

namespace WebTemplate.Database.Configurations
{
    internal class TestModelConfiguration : EntityTypeConfiguration<TestModel>
    {
        public TestModelConfiguration()
        {
            ToTable("TestModels");
            HasKey(s => s.Id);
        }
    }
}
