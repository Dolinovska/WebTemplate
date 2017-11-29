using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace WebTemplate.MVC.Controllers
{
    using PagedList;
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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var articles = this._repository.GetAll<Article>();

            /*if (!string.IsNullOrEmpty(category))
            {
                categoryNews = this._repository.GetAll<Category>().FirstOrDefault(c => c.Name == category)?.News
                               ?? new List<Article>();
            }

            if (!string.IsNullOrEmpty(tags))
            {
                categoryNews = categoryNews.Where(n => FilterByTags(n, tags)).ToList();
            }*/

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.ViewsSortParm = sortOrder == "Views" ? "view_desc" : "Views";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.Text.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    articles = articles.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    articles = articles.OrderBy(s => s.PublishDate);
                    break;
                case "date_desc":
                    articles = articles.OrderByDescending(s => s.PublishDate);
                    break;
                case "Views":
                    articles = articles.OrderBy(s => s.ViewsCount);
                    break;
                case "views_desc":
                    articles = articles.OrderByDescending(s => s.ViewsCount);
                    break;
                default:  // Name ascending 
                    articles = articles.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(articles.ToPagedList(pageNumber, pageSize));

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
