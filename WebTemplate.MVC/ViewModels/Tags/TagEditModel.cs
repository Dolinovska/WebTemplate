using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebTemplate.Database.Models;

namespace WebTemplate.MVC.ViewModels.Tags
{
    using News = WebTemplate.Database.Models.News;

    public class TagEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "News")]
        public int[] SelectedNewsIds { get; set; }
        public IEnumerable<Checkbox> NewsCheckboxes { get; set; }

        public TagEditModel()
        {
        }

        public TagEditModel(Tag tag, IEnumerable<News> allNews)
        {
            this.Id = tag.Id;
            this.Name = tag.Name;
            this.NewsCheckboxes = allNews.Select(t => new Checkbox(t.Title, t.Id.ToString(), false)).ToList();

            this.NewsCheckboxes.Where(pc => tag.News.Any(p => p.Title.Equals(pc.Name, StringComparison.OrdinalIgnoreCase))).ToList()
                .ForEach(c => c.IsChecked = true);
        }
    }
}