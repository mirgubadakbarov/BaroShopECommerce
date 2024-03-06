using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Question
{
    public class QuestionUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Categories")]
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}
