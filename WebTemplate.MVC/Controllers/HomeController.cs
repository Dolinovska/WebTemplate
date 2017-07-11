using System.Web.Mvc;
using WebTemplate.IServices;

namespace WebTemplate.MVC.Controllers
{
    public class TestModelController : Controller
    {
        private ITestModelService _testModelService;

        public TestModelController(ITestModelService testModelService)
        {
            _testModelService = testModelService;
        }

        public ViewResult Index()
        {
            var models = _testModelService.GetAll();
            return View(models);
        }
    }
}