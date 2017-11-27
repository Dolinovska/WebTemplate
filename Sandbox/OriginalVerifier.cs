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

            var duplicates = new List<Article>();
            foreach (var dbArticle in allArticles)
            {
                if (article.Source == dbArticle.Source || article.Text == null ||
                    dbArticle.Text == null)
                {
                    continue;
                }

                Console.WriteLine($"Compare {article.Id} with {dbArticle.Id}");

                if (article.Text.SimilarTo(dbArticle.Text))
                {
                    duplicates.Add(dbArticle);
                }

            }

            if (duplicates.Any())
            {
                article.IsOriginal = false;
                article.Duplicates = string.Join(",", duplicates.Select(d => d.Id));
            }

            else
            {
                article.IsOriginal = true;
                article.Duplicates = null;
            }
            return article;
        }
    }
}