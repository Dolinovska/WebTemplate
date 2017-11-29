using System.Web.Mvc;
using WebTemplate.Database;

namespace WebTemplate.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

    }
}
