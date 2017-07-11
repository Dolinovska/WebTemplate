using System.Data.Entity;
using WebTemplate.Models;

namespace WebTemplate.Database
{
    public class ProjectContextInitializer : DropCreateDatabaseIfModelChanges<ProjectContext>
    {
        public override void InitializeDatabase(ProjectContext context)
        {
            context.TestModels.Add(new TestModel { Property1 = "P1_1", Property2 = "P2_1" });
            context.TestModels.Add(new TestModel { Property1 = "P1_2", Property2 = "P2_2" });
            context.TestModels.Add(new TestModel { Property1 = "P1_3", Property2 = "P2_3" });
            context.SaveChanges();

            base.InitializeDatabase(context);
        }
    }
}