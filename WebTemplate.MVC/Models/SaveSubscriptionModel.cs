using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTemplate.MVC.Models
{
    public class Keys
    {
        public string p256dh { get; set; }
        public string auth { get; set; }
    }

    public class SaveSubscriptionModel
    {
        public string endpoint { get; set; }
        public object expirationTime { get; set; }
        public Keys keys { get; set; }
    }
}