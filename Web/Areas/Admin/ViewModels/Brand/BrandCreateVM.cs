using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.Brand
{
    public class BrandCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
