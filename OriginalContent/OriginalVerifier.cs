using System.Collections.Generic;
using System.Linq;
using WebTemplate.Database.Models;

namespace OriginalContent
{
    public class OriginalVerifier : IOriginalVerifier
    {
        public IEnumerable<News> Verify(IEnumerable<News> newsToFilter)
        {
            var resultNews = new List<News>();

            foreach (var news in newsToFilter)
            {
                var duplicates = newsToFilter.Where(n => n.Id != news.Id && n.Text.SimilarTo(news.Text));

                if (duplicates.Any())
                {
                    news.IsOriginal = false;
                    news.Duplicates = string.Join(",", duplicates.Select(d => d.Id));
                }

                else
                {
                    news.IsOriginal = true;
                    news.Duplicates = null;

                    resultNews.Add(news);
                }           
            }

            return resultNews;
        }
    }
}