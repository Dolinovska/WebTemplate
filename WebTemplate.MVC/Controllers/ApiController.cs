using System.Web.Mvc;
using WebTemplate.Database;
using WebTemplate.Database.Models;
using WebTemplate.MVC.Models;

namespace WebTemplate.MVC.Controllers
{
    public class ApiController : Controller
    {
        private readonly Repository _repository;

        public ApiController()
        {
            _repository = new Repository();
        }

        [HttpPost]
        public JsonResult SaveSubscription(SaveSubscriptionModel saveSubscriptionModel)
        {
            var subscription = new Subscription();

            subscription.Auth = saveSubscriptionModel.keys?.auth;
            subscription.P256dh = saveSubscriptionModel.keys?.p256dh;
            subscription.Endpoint = saveSubscriptionModel?.endpoint;

            _repository.Add(subscription);
            _repository.SaveChanges();



            return Json(new { ok = true });
        }
    }
}