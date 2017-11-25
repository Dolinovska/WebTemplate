using WebTemplate.Database.Models;

namespace WebTemplate.MVC.ViewModels.Categories
{
    public class CategoryEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryEditModel()
        {
        }

        public CategoryEditModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    }
}