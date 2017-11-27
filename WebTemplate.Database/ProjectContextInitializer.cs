using System.Collections.Generic;
using System.Data.Entity;
using WebTemplate.Database.Models;

namespace WebTemplate.Database
{
    public class WebTemplateContextInitializer : DropCreateDatabaseIfModelChanges<WebTemplateContext>
    {
        protected override void Seed(WebTemplateContext context)
        {
            var category = new Category
            {
                Name = "All",
            };
            context.Categories.Add(category);
            context.SaveChanges();
        }
    }
}