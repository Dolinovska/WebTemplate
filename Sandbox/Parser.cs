using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using WebTemplate.Database.Models;

namespace WebTemplate.Parser
{
    public class Parser
    {
        public string Source { get; }
        public string Url { get; }

        public Parser(string source, string url)
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

                foreach (var item in feed.Items)
                {
                    var article = new Article
                    {
                        Title = item.Title.Text,
                        Summary = item.Summary?.Text,
                        Tags = string.Join(Article.TagsSeparator.ToString(), item.Categories.Select(c => c.Name)),
                        PublishDate = item.PublishDate.UtcDateTime,
                        OriginalUrl = item.Id,
                        Source = Source
                    };

                    foreach (SyndicationElementExtension ext in item.ElementExtensions)
                    {
                        if (ext.GetObject<XElement>().Name.LocalName == "encoded")
                            article.Text = ext.GetObject<XElement>().Value;
                    }

                    result.Add(article);
                }
            }
            return result;
        }
    }
}