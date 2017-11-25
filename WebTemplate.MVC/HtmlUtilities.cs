using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WebTemplate.MVC.ViewModels;

namespace WebTemplate.MVC
{


    public static class HtmlUtilities
    {
        public static MvcHtmlString CheckBoxList(this HtmlHelper html, string name, IEnumerable<Checkbox> data)
        {
            var stringBuilder = new StringBuilder();
            var alphanumericComparer = new AlphanumericComparer();
            foreach (var item in data.OrderBy(i => i.Name, alphanumericComparer))
            {
                stringBuilder.Append(GenerateCheckBoxCode(item, name, null));
                stringBuilder.Append(GenerateLabelCode(item));
                stringBuilder.Append("<br />");
            }

            return new MvcHtmlString(stringBuilder.ToString());
        }

        private static string GenerateCheckBoxCode(Checkbox item, string name, int? matchedItems)
        {
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "checkbox";
            builder.Attributes["name"] = name;
            builder.GenerateId(item.Name);
            builder.Attributes["value"] = item.Value;
            if (item.IsChecked)
                builder.Attributes["checked"] = "checked";

            if (matchedItems.HasValue && matchedItems.Value == 0)
                builder.Attributes["disabled"] = "disabled";
            return builder.ToString(TagRenderMode.Normal);
        }

        private static string GenerateLabelCode(Checkbox item)
        {
            var builder = new TagBuilder("label");
            builder.Attributes["for"] = item.Name;
            builder.SetInnerText(item.Name);
            return builder.ToString(TagRenderMode.Normal);
        }

    }
}