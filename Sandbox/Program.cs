﻿using Parser;
using System;

using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            //IParser parser = new VezhaParser();

            //var rssLink = "http://www.vezha.org/feed/";
            //var articles = parser.Parse(rssLink);

            //var repo = new Repository();

            //foreach (var article in articles)
            //{
            //    article.Category = repo.Find<Category>(1);
            //    repo.Add(article);

            //    Console.WriteLine(article.Title + " " + article.Text);
            //}

            //repo.SaveChanges();

            var parser = new GalkaParser();

            var rssLink = "http://www.galka.if.ua/feed/";
            var articles = parser.Parse(rssLink);

            var repo = new Repository();

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