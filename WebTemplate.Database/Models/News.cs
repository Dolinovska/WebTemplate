namespace WebTemplate.Database.Models
{
    using System.Collections.Generic;

    // one to many with category
    // many to many with tags
    public class News
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual decimal Price { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}