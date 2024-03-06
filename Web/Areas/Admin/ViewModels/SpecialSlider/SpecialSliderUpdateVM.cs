using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.SpecialSlider
{
    public class SpecialSliderUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(25)]
        public string Title { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ButtonLink { get; set; }
        public IFormFile? Photo { get; set; }

    }
}
