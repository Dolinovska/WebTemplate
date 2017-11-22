using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebTemplate.Database.Models;

namespace WebTemplate.MVC.ViewModels.Tags
{
    public class TagEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Products")]
        public int[] SelectedProductsIds { get; set; }
        public IEnumerable<Checkbox> ProductsCheckboxes { get; set; }

        public TagEditModel()
        {
        }

        public TagEditModel(Tag tag, IEnumerable<Product> allProducts)
        {
            this.Id = tag.Id;
            this.Name = tag.Name;
            this.ProductsCheckboxes = allProducts.Select(t => new Checkbox(t.Name, t.Id.ToString(), false)).ToList();

            this.ProductsCheckboxes.Where(pc => tag.Products.Any(p => p.Name.Equals(pc.Name, StringComparison.OrdinalIgnoreCase))).ToList()
                .ForEach(c => c.IsChecked = true);
        }
    }
}