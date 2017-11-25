namespace WebTemplate.Database.Models
{
    // one to many with category
    // many to many with tags
    public class News
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual string Text { get; set; }

        public virtual Category Category { get; set; }

        public virtual string Tags { get; set; }

        public static char TagsSeparator = ',';
    }
}