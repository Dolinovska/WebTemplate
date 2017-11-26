using PushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTemplate.Database.Models;

namespace Sandbox
{
    public class Gcm
    {
        public void Run()
        {
            var p = new WebPushNotification();
            var s = new Subscription()
            {
                Auth = "gWlCmj7czCLNWbrnkh1C0g==",
                Endpoint = "https://fcm.googleapis.com/fcm/send/dD-IYOTwtAs:APA91bFyUnZyZdyWLW3blQGFJWMhruvQhTb86Jsmdi0Em3-yqhG7II65vVnZXihWBz_uNyGoE7Di3lvvDMPDnsFQZTUG8kpgv_S06pbgLtCyGj-3aj3-LbknkNmxstpvIzMoYYCrfuW1",
                P256dh = "BCYdOni7OF7gKRNmG345Psge3SgvqHd9pu79hhWKyQhqROIAj9hsABKhRR2fmMSVUbyzlZ6HFUqQNknOd5FD-64="
            };

            p.Push(s, "TEST PUSH!!!");
        }
    }
}
