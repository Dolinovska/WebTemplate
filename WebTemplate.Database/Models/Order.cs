namespace WebTemplate.Database.Models
{
    using System;

    // one to many with category
    // many to many with tags
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Text { get; set; }
        public virtual string Number { get; set; }

    }
}