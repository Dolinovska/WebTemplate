using System;

namespace WebTemplate.Database.Models
{
    public class Subscription
    {
        public virtual int Id { get; set; }

        public string Endpoint { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
        public DateTime? ExpirationTime { get; set; }
    }
}
