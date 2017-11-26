using System;
using WebPush;
using WebTemplate.Database.Models;

namespace PushNotification
{
    public class WebPushNotification : IPushNotification
    {
        public void Push(Subscription sub, string message)
        {
            var pushEndpoint = sub.Endpoint;
            var p256dh = sub.P256dh;
            var auth = sub.Auth;

            var subject = "mailto:example@example.com";
            var publicKey = @"BOf0gGLQJsTBwTuf_SmOebXFVY2Q_hs7WkOxaUJXEvKTaT7NF_WuE4t8xAHvooTsZVDxEMWkBSVTNGC6VtOF-i0";
            var privateKey = @"-S5wUDqf5I_dBP1FnIRKFve6Lw9ekfZWnZ2me1v3cQ4";

            var subscription = new PushSubscription(pushEndpoint, p256dh, auth);
            var vapidDetails = new VapidDetails(subject, publicKey, privateKey);
            //var gcmAPIKey = @"[your key here]";

            var webPushClient = new WebPushClient();
            try
            {
                webPushClient.SendNotification(subscription, message, vapidDetails);
                //webPushClient.SendNotification(subscription, "payload", gcmAPIKey);
            }
            catch (WebPushException exception)
            {
                Console.WriteLine("Http STATUS code" + exception.StatusCode);
            }
        }
    }
}