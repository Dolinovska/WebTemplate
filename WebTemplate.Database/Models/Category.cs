namespace WebTemplate.Database.Models
{
    using System.Collections.Generic;

    // one to many with products
    public class Category
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
