using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Fact
{
    public class FactCreateVM
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
