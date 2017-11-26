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
        [Required]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }
        [Required]
        [Display(Name = "Теги")]
        public string Tags { get; set; }

        public string OriginalUrl { get; set; }

        public string Summary { get; set; }

        public DateTime? PublishDate { get; set; }

        public int ViewsCount { get; set; }

        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        public string Source { get; set; }


        [Required]
        [Display(Name = "Категорія")]
        public int SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
        [Display(Name = "Cвітлина")]
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