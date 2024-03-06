using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Web.Areas.Admin.ViewModels.Testimonial
{
    public class TestimonialCreateVM
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(120)]
        public string Description { get; set; }
        [Required]
        public IFormFile UserPhoto { get; set; }
    }
}
