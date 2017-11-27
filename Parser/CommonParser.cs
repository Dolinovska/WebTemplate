using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using WebTemplate.Database.Models;

namespace Parser
{
    public abstract class CommonParser
    {
        public string Source { get; }
        public string Url { get; }

        protected CommonParser(string source, string url)
        {
            Source = source;
            Url = url;
        }

        public List<Article> Parse()
        {
            var result = new List<Article>();

            using (var reader = XmlReader.Create(Url))
            {
                var feed = SyndicationFeed.Load(reader);
                var xmldoc = XDocument.Load(Url);

                foreach (var item in feed.Items)
                {
                    var article = new Article
                    {
                        Title = item.Title.Text,
                        Summary = item.Summary.Text,
                        Tags = string.Join(Article.TagsSeparator.ToString(), item.Categories.Select(c => c.Name)),
                        PublishDate = item.PublishDate.UtcDateTime,
                        OriginalUrl = item.Id,
                        Source = Source
                    };


                    // Set article text
                    XNamespace content = "http://purl.org/rss/1.0/modules/content/";
                    var rss = xmldoc.Element("rss");
                    var channel = rss.Element("channel");
                    var elements = channel.Elements("item").ToList();
                    var txt = elements.FirstOrDefault(e => e.Element("guid").Value == item.Id).Element(content.GetName("encoded")).Value;
                    article.Text = txt;


                    result.Add(article);
                }
            }
            return result;
        }
    }
}