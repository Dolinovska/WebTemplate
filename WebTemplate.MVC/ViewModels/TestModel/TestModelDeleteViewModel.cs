using System.ComponentModel.DataAnnotations;

namespace WebTemplate.MVC.ViewModels.TestModel
{
    public class TestModelDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}