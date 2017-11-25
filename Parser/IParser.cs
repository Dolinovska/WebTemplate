using System.Collections.Generic;
using WebTemplate.Database.Models;

namespace Parser
{
    public interface IParser
    {
        List<News> Parse(string url);
    }
}
