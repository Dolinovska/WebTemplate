using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTemplate.Database;
using WebTemplate.Database.Models;
using WebTemplate.MVC.ViewModels.Categories;
using WebTemplate.MVC.ViewModels.Products;

namespace WebTemplate.MVC.Controllers
{
    using WebTemplate.MVC.ViewModels.Tags;

    public class TagsController : Controller
    {
        private readonly Repository _repository;

        public TagsController()
        {
            _repository = new Repository();
        }

        public ActionResult Index()
        {
            var tags = _repository.GetAll<Tag>().ToList();
            return View(tags);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tag = _repository.Find<Tag>(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(tag);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = _repository.Find<Tag>(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            var allProducts = _repository.GetAll<Product>();
            var tagEditModel = new TagEditModel(tag, allProducts);
            return View(tagEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TagEditModel tagEditModel)
        {
            var tag = this._repository.Find<Tag>(tagEditModel.Id);

            if (ModelState.IsValid)
            {
                tag.Name = tagEditModel.Name;
                tag.Products.Clear();

                tagEditModel.SelectedProductsIds.Select(id => _repository.Find<Product>(id)).ToList()
                    .ForEach(p => tag.Products.Add(p));

                _repository.Update(tag);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }
            var allProducts = _repository.GetAll<Product>();
            tagEditModel = new TagEditModel(tag, allProducts);
            return View(tagEditModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = _repository.Find<Tag>(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tag = _repository.Find<Tag>(id);
            _repository.Remove(tag);
            _repository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
