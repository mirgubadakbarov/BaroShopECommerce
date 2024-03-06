using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Brand
{
    public class BrandUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
