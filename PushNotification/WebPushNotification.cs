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

            var subject = "dmytro.tsiniavskyi@gmail.com";
            var publicKey = @"BFT8Qom635-BGZB88CrAGF9WB20eH2fl7MWzh03UmnWhJqdQTlgVzAXVfHCR4etLnSH17wiFkdkzUHTOb7BHyLI";
            var privateKey = @"64_ZQPOFquTjjXDkFrBFf9yAIZa639bQ1QMDLQpzilk";

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