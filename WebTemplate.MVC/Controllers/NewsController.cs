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

    public class NewsController : Controller
    {
        private readonly Repository _repository;

        public NewsController()
        {
            _repository = new Repository();
        }

        [HttpGet]
        public ActionResult Index(string category, string tags)
        {
            var categoryNews = this._repository.GetAll<News>();

            if (!string.IsNullOrEmpty(category))
            {
                categoryNews = this._repository.GetAll<Category>().FirstOrDefault(c => c.Name == category)?.News
                               ?? new List<News>();
            }
            if (!string.IsNullOrEmpty(tags))
            {
                categoryNews = categoryNews.Where(n => FilterByTags(n, tags)).ToList();
            }
            return View(categoryNews);
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
            var news = new News();
            var allCategories = _repository.GetAll<Category>();
            var newsEditModel = new NewsEditModel(news, allCategories);
            return View(newsEditModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsEditModel newsEditModel)
        {
            var news = new News();

            if (ModelState.IsValid)
            {
                news.Title = newsEditModel.Title;
                news.Text = newsEditModel.Text;
                news.Tags = newsEditModel.Tags;

                news.Category = this._repository.Find<Category>(newsEditModel.SelectedCategory);
                _repository.Add(news);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }

            var allCategories = _repository.GetAll<Category>();
            newsEditModel = new NewsEditModel(news, allCategories);
            return View(newsEditModel);
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
            var allCategories = _repository.GetAll<Category>();
            var newsEditModel = new NewsEditModel(news, allCategories);
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
                news.Text = newsEditModel.Text;
                news.Tags = newsEditModel.Tags;

                news.Category = this._repository.Find<Category>(newsEditModel.SelectedCategory);
                _repository.Update(news);
                _repository.SaveChanges();
                return RedirectToAction("Index");
            }

            var allCategories = _repository.GetAll<Category>();
            newsEditModel = new NewsEditModel(news, allCategories);
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

        private bool FilterByTags(News news, string tags)
        {
            var separatedTags = tags.Split(News.TagsSeparator);
            var newsTags = news.Tags.Split(News.TagsSeparator);

            return newsTags.Any(newsTag => separatedTags.Contains(newsTag));
        }
    }
}
