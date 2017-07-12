using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using WebTemplate.IServices;
using WebTemplate.Models;
using WebTemplate.MVC.ViewModels.TestModel;

namespace WebTemplate.MVC.Controllers
{
    public class TestModelController : Controller
    {
        private readonly ITestModelService _testModelService;

        public TestModelController(ITestModelService testModelService)
        {
            _testModelService = testModelService;
        }

        public ViewResult Index()
        {
            var models = _testModelService.GetAll();
            var listViewModels = Mapper.Map<List<TestModelListViewModel>>(models);
            return View(listViewModels);
        }

        public ViewResult Details(TestModelSearchModel searchModel)
        {
            var model = _testModelService.Find(searchModel.Id);
            var detailsViewModel = Mapper.Map<TestModelDetailsViewModel>(model);
            return View(detailsViewModel);
        }

        public ActionResult Create()
        {
            var createViewModel = new TestModelCreateViewModel();
            return View(createViewModel);
        }

        [HttpPost]
        public ActionResult Create(TestModelCreateViewModel createViewModel)
        {
            if (!ModelState.IsValid)
                return View(createViewModel);

            var model = Mapper.Map<TestModel>(createViewModel);
            _testModelService.Create(model);

            return RedirectToAction("Index");
        }

        public ActionResult Update(TestModelSearchModel searchModel)
        {
            var model = _testModelService.Find(searchModel.Id);

            if (model == null)
                return HttpNotFound();

            var updateViewModel = Mapper.Map<TestModelUpdateViewModel>(model);
            return View(updateViewModel);
        }

        [HttpPost]
        public ActionResult Update(TestModelUpdateViewModel updateViewModel)
        {
            if (!ModelState.IsValid)
                return View(updateViewModel);

            var model = Mapper.Map<TestModel>(updateViewModel);
            _testModelService.Update(model);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(TestModelSearchModel searchModel)
        {
            var model = _testModelService.Find(searchModel.Id);

            if (model == null)
                return HttpNotFound();

            var deleteViewModel = Mapper.Map<TestModelDeleteViewModel>(model);

            return View(deleteViewModel);
        }

        [HttpPost]
        public ActionResult Delete(TestModelDeleteViewModel deleteModel)
        {
            if (!ModelState.IsValid)
                return View(deleteModel);

            var model = _testModelService.Find(deleteModel.Id);

            if (model == null)
                return HttpNotFound();

            _testModelService.Delete(model);

            return RedirectToAction("Index");
        }
    }
}