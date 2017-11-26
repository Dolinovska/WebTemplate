using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public static class Extensions
    {
        public static IEnumerable<HtmlNode> Spans(this HtmlDocument source)
        {
            if (source == null) return Enumerable.Empty<HtmlNode>();
            return source.DocumentNode.Descendants().Spans();
        }

        public static IEnumerable<HtmlNode> Spans(this IEnumerable<HtmlNode> source)
        {
            return source.Where(d => d.Name == "span");
        }

        public static IEnumerable<HtmlNode> WithClass(this IEnumerable<HtmlNode> source, string cssClass)
        {
            return source.Where(d => d.Attributes["class"] != null && d.Attributes["class"].Value.Contains(cssClass));
        }

        public static IEnumerable<HtmlNode> Images(this HtmlNode source)
        {
            if (source == null) return Enumerable.Empty<HtmlNode>();
            return source.Descendants().Images();
        }

        public static IEnumerable<HtmlNode> Images(this IEnumerable<HtmlNode> source)
        {
            return source.Where(d => d.Name == "img");
        }

        public static IEnumerable<HtmlNode> Images(this HtmlDocument source)
        {
            if (source == null) return Enumerable.Empty<HtmlNode>();
            return source.DocumentNode.Descendants().Images();
        }

        public static string Attr(this HtmlNode source, string attr)
        {
            if (source == null) return string.Empty;
            var attribute = source.Attributes[attr];
            return attribute == null ? string.Empty : source.Attributes[attr].Value;
        }

        public static string Href(this HtmlNode source)
        {
            return source.Attr("href");
        }

        public static string Src(this HtmlNode source)
        {
            return source.Attr("src");
        }

        public static IEnumerable<HtmlNode> Divs(this HtmlDocument source)
        {
            if (source == null) return Enumerable.Empty<HtmlNode>();
            return source.DocumentNode.Descendants().Divs();
        }

        public static IEnumerable<HtmlNode> Divs(this IEnumerable<HtmlNode> source)
        {
            return source.Where(d => d.Name == "div");
        }

        //public static IEnumerable<HtmlNode> Images(this HtmlNode source)
        //{
        //    if (source == null) return Enumerable.Empty<HtmlNode>();
        //    return source.Descendants().Images();
        //}

        //public static IEnumerable<HtmlNode> Images(this IEnumerable<HtmlNode> source)
        //{
        //    return source.Where(d => d.Name == "img");
        //}

    }
}
