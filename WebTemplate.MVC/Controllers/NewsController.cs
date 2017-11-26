using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace WebTemplate.MVC.Controllers
{
    using Images;
    using PushNotification;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using WebTemplate.MVC.ViewModels;
    using WebTemplate.MVC.ViewModels.Newss;

    public class NewsController : Controller
    {
        private readonly Repository _repository;
        private readonly IImageManager _imageManager;
        private readonly IPushNotification _pushNotification;

        public NewsController()
        {
            _repository = new Repository();
            _imageManager = new ImageManager();
            _pushNotification = new WebPushNotification();
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

            categoryNews = FilterBySimilarContent(categoryNews);

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

            news.ViewsCount++;

            _repository.Update(news);
            _repository.SaveChanges();

            var allNews = this._repository.GetAll<News>().Where(n => n.Id != news.Id);

            var newsDetailsModel = new NewsDetailsModel(news);
            foreach (var otherNews in allNews)
            {
                if (otherNews.Text.SimilarTo(news.Text))
                {
                    newsDetailsModel.DuplicateNews.Add(new DuplicateNews
                    {
                        OriginalUrl = otherNews.OriginalUrl,
                        Source = otherNews.Source
                    });
                }
            }
            return View(newsDetailsModel);
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
                news.Image = _imageManager.Save(newsEditModel.PostedImage);

                _repository.Add(news);
                _repository.SaveChanges();

                // TODO: Implement!!!
                var subscription = _repository.GetAll<Subscription>().LastOrDefault();
                _pushNotification.Push(subscription, news.Title);

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
                news.Image = _imageManager.Save(newsEditModel.PostedImage);

                _repository.Update(news);
                _repository.SaveChanges();

                return RedirectToAction("Index", "News");
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

        public ActionResult AboutUs()
        {
            return View();
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

        [ChildActionOnly]
        public PartialViewResult PopularTags()
        {
            var allTags = this._repository.GetAll<News>().SelectMany(n => n.Tags.Split(News.TagsSeparator));
            var tagStat = allTags.GroupBy(t => t)
                .Select(group => new TagStat { Tag = group.Key, Count = group.Count() })
                .OrderByDescending(t => t.Count)
                .Take(5);

            return PartialView(tagStat);
        }

        [ChildActionOnly]
        public PartialViewResult PopularNews()
        {
            var popularNews = this._repository.GetAll<News>().OrderByDescending(n => n.ViewsCount).Take(4);
            return PartialView(popularNews);
        }

        [ChildActionOnly]
        public PartialViewResult LatestNews()
        {
            var latestNews = this._repository.GetAll<News>().OrderByDescending(n => n.PublishDate).Take(4);
            return PartialView(latestNews);
        }

        [ChildActionOnly]
        public PartialViewResult Counters()
        {
            var news = this._repository.GetAll<News>();
            var counters = new Counters
            {
                NewsCount = news.Count(),
                SourcesCount = news.Select(n => n.Author).Distinct().Count(),
                TagsCount = news.SelectMany(n => n.Tags.Split(News.TagsSeparator)).Distinct().Count(),
                ViewsCount = news.Sum(n => n.ViewsCount)
            };

            return PartialView(counters);
        }

        private bool FilterByTags(News news, string tags)
        {
            var separatedTags = tags.Split(News.TagsSeparator);
            var newsTags = news.Tags.Split(News.TagsSeparator);

            return newsTags.Any(newsTag => separatedTags.Contains(newsTag));
        }

        private IEnumerable<News> FilterBySimilarContent(IEnumerable<News> newsToFilter)
        {
            var resultNews = new List<News>();
            foreach (var news in newsToFilter)
            {
                if (!resultNews.Any(rn => rn.Text.SimilarTo(news.Text)))
                {
                    resultNews.Add(news);
                }
            }
            return resultNews;
        }
    }
}
