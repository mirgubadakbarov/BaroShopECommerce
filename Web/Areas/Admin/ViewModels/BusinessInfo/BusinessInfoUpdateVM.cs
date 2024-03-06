using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.BusinessInfo
{
    public class BusinessInfoUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
