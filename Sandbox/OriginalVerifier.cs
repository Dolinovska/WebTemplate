using System;
using System.Collections.Generic;
using System.Linq;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace WebTemplate.Parser
{
    public class OriginalVerifier
    {
        public static Article Verify(Article article)
        {
            var repository = new Repository();
            var allArticles = repository.GetAll<Article>();

            article.IsOriginal = true;
            foreach (var dbArticle in allArticles)
            {
                if (article.Source == dbArticle.Source || article.Title == null || dbArticle.Title == null)
                {
                    continue;
                }

                Console.WriteLine($"Compare {article.Id} with {dbArticle.Id}");

                if (article.Title.SimilarTo(dbArticle.Title))
                {
                    article.IsOriginal = false;
                    article.OriginalActicleId = dbArticle.Id;
                    break;
                }
            }
            return article;
        }
    }
}