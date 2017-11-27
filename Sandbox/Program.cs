using System;
using System.Collections.Generic;
using System.Linq;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace WebTemplate.Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new Repository();

            var parsers = new List<Parser>
            {
                new Parser("Галка", "http://www.galka.if.ua/feed/"),
               /* new Parser("Вежа", "http://www.vezha.org/feed/"),*/
                new Parser("Фіртка", "http://firtka.if.ua/rss/"),
                new Parser("0342", "https://www.0342.ua/rss"),
                new Parser("Мі100", "http://mi100.info/feed/"),
                new Parser("Версії", "http://versii.if.ua/feed/"),
                new Parser("Вестньюз", "https://westnews.com.ua/feed/"),
                new Parser("Галицький кореспондент", "http://gk-press.if.ua/feed/"),
                new Parser("Курс", "http://kurs.if.ua/rss/export.xml"),
                new Parser("Франківчани", "http://frankivchany.if.ua/?format=feed&type=rss"),
                new Parser("Бліц", "http://www.blitz.if.ua/include/rss/rss.php"),
                new Parser("Правда", "http://pravda.if.ua/rss.php"),
                new Parser("Бріз", "http://briz.if.ua/rss/"),
            };

            foreach (var parser in parsers)
            {
                var articles = parser.Parse();
                foreach (var article in articles)
                {
                    article.Category = repository.GetAll<Category>().FirstOrDefault();

                    var dbArticle = repository.GetAll<Article>()
                        .FirstOrDefault(a => a.OriginalUrl == article.OriginalUrl);

                    if (dbArticle == null)
                    {
                        Console.WriteLine("Verify " + article.OriginalUrl);
                        OriginalVerifier.Verify(article);
                        Console.WriteLine(article.OriginalUrl + " added");

                        repository.Add(article);
                        repository.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine(article.OriginalUrl + " skipped");
                    }
                }
            }
        }
    }
}