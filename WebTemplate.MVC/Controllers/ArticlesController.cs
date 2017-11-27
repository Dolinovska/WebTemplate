using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace WebTemplate.MVC.Controllers
{
    using System;
    using System.Collections.Generic;

    public class ArticlesController : Controller
    {
        private readonly Repository _repository;

        public ArticlesController()
        {
            _repository = new Repository();
        }

        [HttpGet]
        public ActionResult Index(string category, string tags)
        {
            var categoryNews = this._repository.GetAll<Article>();

            if (!string.IsNullOrEmpty(category))
            {
                categoryNews = this._repository.GetAll<Category>().FirstOrDefault(c => c.Name == category)?.News
                               ?? new List<Article>();
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

            var news = _repository.Find<Article>(id);

            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

       

        private bool FilterByTags(Article article, string tags)
        {
            var separatedTags = tags.Split(Article.TagsSeparator);
            var newsTags = article.Tags.Split(Article.TagsSeparator);

            return newsTags.Any(newsTag => separatedTags.Any(st => st.Equals(newsTag, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}
