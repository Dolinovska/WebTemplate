namespace WebTemplate.Database.Models
{
    using System.Collections.Generic;

    // many to many with products
    public class Tag
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}