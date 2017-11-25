using System.Data.Entity;

namespace WebTemplate.Database
{
    using System.Collections.Generic;

    using WebTemplate.Database.Models;

    public class WebTemplateContextInitializer : DropCreateDatabaseIfModelChanges<WebTemplateContext>
    {
        protected override void Seed(WebTemplateContext context)
        {
            var category1 = new Category { Name = "Category 1" };
            var category2 = new Category { Name = "Category 2" };

            var news11 = new News { Title = "News 11", Text = "11", Tags = "Tag1, Tag2"};
            var news12 = new News { Title = "News 12", Text = "11", Tags = "Tag1, Tag3" };
            var news21 = new News { Title = "News 21", Text = "21", Tags = "Tag2, Tag3" };
            var news22 = new News { Title = "News 22", Text = "22", Tags = "Tag4, Tag3" };

            category1.News = new List<News> { news11, news12 };
            category2.News = new List<News> { news21, news22 };


            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.SaveChanges();
        }
    }
}