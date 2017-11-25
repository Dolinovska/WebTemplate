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
        public string Title { get; set; }
        public string Text { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }


        [Display(Name = "Category")]
        public int SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        public NewsEditModel()
        {
        }

        public NewsEditModel(News news, IEnumerable<Category> allCategories)
        {
            this.Id = news.Id;
            this.Title = news.Title;
            this.Text = news.Text;
            this.Tags = news.Tags;

            this.Categories = allCategories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = c.Id == this.SelectedCategory
            });
        }
    }
}