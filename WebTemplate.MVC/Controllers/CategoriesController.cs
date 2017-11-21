using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WebTemplate.MVC.Controllers
{
    using Microsoft.Ajax.Utilities;

    using WebTemplate.Database;
    using WebTemplate.Database.Models;
    using WebTemplate.MVC.ViewModels.Categories;

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

            var allProducts = _repository.GetAll<Product>();
            var categoryEditModel = new CategoryEditModel(category, allProducts);
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
                category.Products.Clear();

                categoryEditModel.SelectedProductIds.Select(id => _repository.Find<Product>(id))
                      .ForEach(p => category.Products.Add(p));

                _repository.Update(category);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }
            var allProducts = _repository.GetAll<Product>();
            categoryEditModel = new CategoryEditModel(category, allProducts);
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
