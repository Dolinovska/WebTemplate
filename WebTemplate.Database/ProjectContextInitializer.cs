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

            var product11 = new Product { Name = "Product 11", Price = 11 };
            var product12 = new Product { Name = "Product 12", Price = 12 };
            var product21 = new Product { Name = "Product 21", Price = 21 };
            var product22 = new Product { Name = "Product 22", Price = 22 };

            category1.Products = new List<Product> { product11, product12 };
            category2.Products = new List<Product> { product21, product22 };

            var tag1 = new Tag { Name = "Tag 1" };
            var tag2 = new Tag { Name = "Tag 2" };

            product11.Tags = new List<Tag> { tag1 };
            product12.Tags = new List<Tag> { tag2 };

            product21.Tags = new List<Tag> { tag1, tag2 };
            product22.Tags = new List<Tag> { tag1, tag2 };


            context.Categories.Add(category1);
            context.Categories.Add(category2);
            context.SaveChanges();
        }
    }
}