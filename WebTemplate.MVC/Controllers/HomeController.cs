using System.Web.Mvc;
using WebTemplate.Database;

namespace WebTemplate.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository _repository;

        public HomeController()
        {
            _repository = new Repository();
        }

        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

    }
}
