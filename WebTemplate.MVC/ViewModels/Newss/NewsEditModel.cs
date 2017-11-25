namespace WebTemplate.MVC.ViewModels.Newss
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using WebTemplate.Database.Models;

    using News = WebTemplate.Database.Models.News;

    public sealed class NewsEditModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Tags { get; set; }

        public string OriginalUrl { get; set; }

        public string Summary { get; set; }

        public DateTime? PublishDate { get; set; }

        public int ViewsCount { get; set; }

        public string Author { get; set; }

        public string Source { get; set; }


        [Display(Name = "Category")]
        public int SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Title Image")]
        public HttpPostedFileBase PostedImage { get; set; }

        public string Image { get; set; }

        public NewsEditModel()
        {
        }

        public NewsEditModel(News news, IEnumerable<Category> allCategories)
        {
            this.Id = news.Id;
            this.Title = news.Title;
            this.Text = news.Text;
            this.Tags = news.Tags;
            this.OriginalUrl = news.OriginalUrl;

            this.Source = news.Source;
            this.Author = news.Author;
            this.Summary = news.Summary;
            this.ViewsCount = news.ViewsCount;
            this.PublishDate = news.PublishDate;
            this.Image = news.Image;

            SelectedCategory = news.Category?.Id ?? 0;

            this.Categories = allCategories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
                Selected = c.Id == this.SelectedCategory
            });
        }
    }
}