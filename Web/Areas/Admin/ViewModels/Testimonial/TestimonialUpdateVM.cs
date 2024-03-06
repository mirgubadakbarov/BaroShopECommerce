using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Testimonial
{
    public class TestimonialUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(120)]
        public string Description { get; set; }
        public IFormFile? UserPhoto { get; set; }
    }
}
