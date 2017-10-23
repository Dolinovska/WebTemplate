using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WebTemplate.MVC.Controllers
{
    using WebTemplate.Database;
    using WebTemplate.Database.Models;

    public class CategoryController : Controller
    {
        private readonly Repository _repository;

        public CategoryController()
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
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(category);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
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
