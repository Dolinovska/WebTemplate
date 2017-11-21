using System;
using System.Collections.Generic;
using System.Linq;

using WebTemplate.Database.Models;

namespace WebTemplate.MVC.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using WebGrease.Css.Extensions;

    public class CategoryEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Products")]
        public int[] SelectedProductIds { get; set; }
        public IEnumerable<Checkbox> ProductCheckboxes { get; set; }

        public CategoryEditModel()
        {
        }

        public CategoryEditModel(Category category, IEnumerable<Product> allProducts)
        {
            Id = category.Id;
            Name = category.Name;
            ProductCheckboxes = allProducts.Select(p => new Checkbox(p.Name, p.Id.ToString(), false)).ToList();

            ProductCheckboxes.Where(pc => category.Products.Any(p => p.Name.Equals(pc.Name, StringComparison.OrdinalIgnoreCase)))
                .ForEach(c => c.IsChecked = true);
        }
    }
}