using System.Data.Entity;

namespace WebTemplate.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WebTemplate.Database.Models;

    public class WebTemplateContextInitializer : DropCreateDatabaseIfModelChanges<WebTemplateContext>
    {
        protected override void Seed(WebTemplateContext context)
        {
            var category = new Category { Name = "All" };
            category.News = new List<News>();

            var random = new Random();

            for (int i = 1; i < 50; i++)
            {
                var n = new News {
                    Title = "News " + i,
                    Text = random.NextString(200),
                    Summary = random.NextString(50),
                    Tags = "Tag1"
                };

                category.News.Add(n);
            }

            context.Categories.Add(category);
            context.SaveChanges();
        }
    }
}