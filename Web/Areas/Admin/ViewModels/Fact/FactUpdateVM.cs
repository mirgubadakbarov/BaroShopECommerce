using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.Fact
{
    public class FactUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
