using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace WebTemplate.MVC.Controllers
{
    using WebTemplate.MVC.ViewModels.Categories;
    using WebTemplate.MVC.ViewModels.Products;

    public class ProductsController : Controller
    {
        private readonly Repository _repository;

        public ProductsController()
        {
            _repository = new Repository();
        }

        public ActionResult Index()
        {
            var products = _repository.GetAll<Product>().ToList();
            return View(products);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _repository.Find<Product>(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(product);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _repository.Find<Product>(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var allTags = _repository.GetAll<Tag>();
            var allCategories = _repository.GetAll<Category>();
            var productEditModel = new ProductEditModel(product, allCategories, allTags);
            return View(productEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditModel productEditModel)
        {
            var product = this._repository.Find<Product>(productEditModel.Id);

            if (ModelState.IsValid)
            {
                product.Name = productEditModel.Name;
                product.Tags.Clear();

                productEditModel.SelectedTagsIds.Select(id => _repository.Find<Tag>(id)).ToList()
                    .ForEach(t => product.Tags.Add(t));

                _repository.Update(product);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }
            var allTags = _repository.GetAll<Tag>();
            var allCategories = _repository.GetAll<Category>();
            productEditModel = new ProductEditModel(product, allCategories, allTags);
            return View(productEditModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _repository.Find<Product>(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _repository.Find<Product>(id);
            _repository.Remove(product);
            _repository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
