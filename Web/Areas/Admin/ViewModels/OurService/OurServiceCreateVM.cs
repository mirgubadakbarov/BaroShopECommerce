using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Web.Areas.Admin.ViewModels.OurService
{
    public class OurServiceCreateVM
    {
        [Required]
        public string Icon { get; set; }
        [Required]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        [MaxLength(70)]
        public string Description { get; set; }
    }
}
