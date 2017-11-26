using WebTemplate.Database.Models;

namespace PushNotification
{
    public interface IPushNotification
    {
        void Push(Subscription sub, string message);
    }
}
