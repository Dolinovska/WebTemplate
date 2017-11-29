namespace WebTemplate.Database.Models
{
    using System;

    // one to many with category
    // many to many with tags
    public class Article
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Text { get; set; }

        public virtual Category Category { get; set; }

        public virtual string Tags { get; set; }

        public virtual string OriginalUrl { get; set; }

        public virtual string Summary { get; set; }

        public virtual DateTime? PublishDate { get; set; }

        public int ViewsCount { get; set; }

        public virtual string Source { get; set; }

        public static char TagsSeparator = ',';

        public virtual bool IsOriginal { get; set; }

        public virtual int OriginalActicleId { get; set; }
    }
}