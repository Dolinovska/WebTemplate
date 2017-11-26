using OriginalContent;
using WebTemplate.Database;
using WebTemplate.Database.Models;

namespace Sandbox
{
    public class OriginalContent
    {
        public void Run()
        {
            var repo = new Repository();
            var news = repo.GetAll<News>();

            var original = new OriginalVerifier();
            var originals = original.Verify(news);

            foreach(var n in news)
                repo.Update(n);

            repo.SaveChanges();
        }
    }
}
