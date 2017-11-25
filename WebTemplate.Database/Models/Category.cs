namespace WebTemplate.Database.Models
{
    using System.Collections.Generic;

    // one to many with news
    public class Category
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
