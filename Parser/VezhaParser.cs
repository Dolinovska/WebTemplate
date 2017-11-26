using HtmlAgilityPack;
using Images;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using WebTemplate.Database.Models;

namespace Parser
{
    public class VezhaParser : IParser
    {
        private readonly IImageManager _imageManager;

        public VezhaParser()
        {
            _imageManager = new ImageManager();
        }

        public List<News> Parse(string url)
        {
            var result = new List<News>();

            using (var reader = XmlReader.Create(url))
            {
                var feed = SyndicationFeed.Load(reader);
                var xmldoc = XDocument.Load(url);

                foreach (var item in feed.Items)
                {
                    var article = new News
                    {
                        Title = item.Title.Text,
                        Summary = item.Summary.Text,
                        Tags = string.Join(News.TagsSeparator.ToString(), item.Categories.Select(c => c.Name)),
                        PublishDate = item.PublishDate.UtcDateTime,
                        OriginalUrl = item.Id,
                        Author = "Телерадіокомпанія \"ВЕЖА\"",
                        Source = "Телерадіокомпанія \"ВЕЖА\""
                    };

                    // Set article text
                    XNamespace content = "http://purl.org/rss/1.0/modules/content/";
                    var rss = xmldoc.Element("rss");
                    var channel = rss.Element("channel");
                    var elements = channel.Elements("item").ToList();
                    var txt = elements.FirstOrDefault(e => e.Element("guid").Value == item.Id).Element(content.GetName("encoded")).Value;
                    article.Text = txt;


                    // Download image
                    var web = new HtmlWeb();
                    var doc = web.Load(item.Id);

                    var img = doc.Divs().WithClass("post").FirstOrDefault().Images().FirstOrDefault();
                    var src = img?.Src();
                    article.Image = _imageManager.Download(src);
                    

                    result.Add(article);
                }
            }

            return result;
        }
    }
}