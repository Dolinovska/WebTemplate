using System;

namespace WebTemplate.Database.Models
{
    // one to many with category
    // many to many with tags
    public class News
    {
        public static char TagsSeparator = ',';


        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Text { get; set; }

        public virtual Category Category { get; set; }

        public virtual string Tags { get; set; }        

        public virtual string OriginalUrl { get; set; }

        public virtual string Summary { get; set; }

        public virtual DateTime? PublishDate { get; set; }

        public virtual string Author { get; set; }

        public virtual string Source { get; set; }

        public virtual int ViewCount { get; set; }
    }
}