namespace WebTemplate.Database.Models
{
    using System.Collections.Generic;

    // one to many with category
    // many to many with tags
    public class News
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Text { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}