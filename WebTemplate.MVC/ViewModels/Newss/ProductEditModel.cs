namespace WebTemplate.MVC.ViewModels.Newss
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using WebTemplate.Database.Models;

    using News = WebTemplate.Database.Models.News;

    public class NewsEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Tags")]
        public int[] SelectedTagsIds { get; set; }
        public IEnumerable<Checkbox> TagsCheckboxes { get; set; }

        [Display(Name = "Category")]
        public int SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public NewsEditModel()
        {
        }

        public NewsEditModel(News news, IEnumerable<Category> allCategories, IEnumerable<Tag> allTags)
        {
            this.Id = news.Id;
            this.Name = news.Name;
            this.Price = news.Price;

            this.TagsCheckboxes = allTags.Select(p => new Checkbox(p.Name, p.Id.ToString(), false)).ToList();
            this.TagsCheckboxes.Where(tc => news.Tags.Any(t => t.Name.Equals(tc.Name, StringComparison.OrdinalIgnoreCase))).ToList()
                .ForEach(c => c.IsChecked = true);

            this.Categories = allCategories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = c.Id == this.SelectedCategory
            });
        }
    }
}