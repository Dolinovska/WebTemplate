using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace WebTemplate.MVC.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq.Expressions;

    using WebTemplate.MVC.ViewModels.Newss;

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
