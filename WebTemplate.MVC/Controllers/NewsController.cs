using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace WebTemplate.MVC.Controllers
{
    using WebTemplate.MVC.ViewModels.Newss;

    public class NewsController : Controller
    {
        private readonly Repository _repository;

        public NewsController()
        {
            _repository = new Repository();
        }

        public ActionResult Index()
        {
            var news = _repository.GetAll<News>().ToList();
            return View(news);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var news = _repository.Find<News>(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(news);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _repository.Find<News>(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            var allTags = _repository.GetAll<Tag>();
            var allCategories = _repository.GetAll<Category>();
            var newsEditModel = new NewsEditModel(news, allCategories, allTags);
            return View(newsEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsEditModel newsEditModel)
        {
            var news = this._repository.Find<News>(newsEditModel.Id);

            if (ModelState.IsValid)
            {
                news.Title = newsEditModel.Title;
                news.Tags.Clear();

                newsEditModel.SelectedTagsIds.Select(id => _repository.Find<Tag>(id)).ToList()
                    .ForEach(t => news.Tags.Add(t));

                _repository.Update(news);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }
            var allTags = _repository.GetAll<Tag>();
            var allCategories = _repository.GetAll<Category>();
            newsEditModel = new NewsEditModel(news, allCategories, allTags);
            return View(newsEditModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _repository.Find<News>(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = _repository.Find<News>(id);
            _repository.Remove(news);
            _repository.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
