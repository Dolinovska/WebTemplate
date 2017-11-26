using System.Collections.Generic;
using WebTemplate.Database.Models;

namespace OriginalContent
{
    public interface IOriginalVerifier
    {
        IEnumerable<News> Verify(IEnumerable<News> newsToFilter);
    }
}