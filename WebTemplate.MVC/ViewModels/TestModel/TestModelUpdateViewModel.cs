using System.ComponentModel.DataAnnotations;

namespace WebTemplate.MVC.ViewModels.TestModel
{
    public class TestModelUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Property1 { get; set; }
        [Required]
        public string Property2 { get; set; }
    }
}