using System.Collections.Generic;

namespace Scraper
{
    public interface IScraper
    {
        List<string> Scrap();
    }
}