﻿using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace Sandbox
{
    public class Parse
    {
        public void Run()
        {
            IParser parser = new VezhaParser();

            var rssLink = "http://www.vezha.org/feed/";
            var articles = parser.Parse(rssLink);

            var repo = new Repository();

            foreach (var article in articles)
            {
                article.Category = repo.Find<Category>(1);
                repo.Add(article);

                Console.WriteLine(article.Title + " " + article.Text);
            }

            repo.SaveChanges();

            parser = new GalkaParser();

            rssLink = "http://www.galka.if.ua/feed/";
            articles = parser.Parse(rssLink);

            repo = new Repository();

            foreach (var article in articles)
            {
                article.Category = repo.Find<Category>(1);
                repo.Add(article);

                Console.WriteLine(article.Title + " " + article.Text);
            }

            repo.SaveChanges();
        }
    }
}