using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTemplate.Database;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            IParser parser = new VezhaParser();

            var rssLink = "http://www.vezha.org/feed/";
            var articles = parser.Parse(rssLink);

            //var context = new WebTemplateContext();

            foreach (var article in articles)
            {
                Console.WriteLine(article.Title + " " + article.Text);
            }
        }
    }
}