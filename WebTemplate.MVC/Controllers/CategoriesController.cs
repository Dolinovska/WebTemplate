using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTemplate.Database;
using WebTemplate.Database.Models;
using WebTemplate.MVC.ViewModels.Categories;

namespace WebTemplate.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly Repository _repository;

        public CategoriesController()
        {
            _repository = new Repository();
        }

        public ActionResult Index()
        {
            var categories = _repository.GetAll<Category>().ToList();
            return View(categories);
        }

        [ChildActionOnly]
        public PartialViewResult MenuList()
        {
            var categories = this._repository.GetAll<Category>();
            return this.PartialView(categories);

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _repository.Find<Category>(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(category);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _repository.Find<Category>(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var categoryEditModel = new CategoryEditModel(category);
            return View(categoryEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryEditModel categoryEditModel)
        {
            var category = this._repository.Find<Category>(categoryEditModel.Id);

            if (ModelState.IsValid)
            {
                category.Name = categoryEditModel.Name;
                _repository.Update(category);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }

            categoryEditModel = new CategoryEditModel(category);
            return View(categoryEditModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _repository.Find<Category>(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _repository.Find<Category>(id);
            _repository.Remove(category);
            _repository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
