using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.BusinessInfo
{
    public class BusinessInfoCreateVM
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

    }


}
