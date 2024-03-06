using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Web.Areas.Admin.ViewModels.SpecialSlider
{
    public class SpecialSliderCreateVM
    {
        [Required, MaxLength(25)]
        public string Title { get; set; }
        public int Order { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ButtonLink { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
