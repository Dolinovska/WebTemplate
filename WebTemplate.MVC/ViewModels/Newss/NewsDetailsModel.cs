using System;

namespace WebTemplate.MVC.ViewModels.Newss
{
    using System.Collections.Generic;

    using WebTemplate.Database.Models;

    public class NewsDetailsModel
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

        public string Category { get; set; }

        public List<DuplicateNews> DuplicateNews { get; set; }

        public virtual string Image { get; set; }


        public NewsDetailsModel()
        {
            DuplicateNews = new List<DuplicateNews>();
        }

        public NewsDetailsModel(News news)
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

            this.Category = news.Category?.Name;

            DuplicateNews = new List<DuplicateNews>();
        }


    }
    public class DuplicateNews
    {
        public string Source { get; set; }
        public string OriginalUrl { get; set; }
    }
}