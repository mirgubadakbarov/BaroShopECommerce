using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.OurService
{
    public class OurServiceUpdateVM
    {
        public int Id { get; set; }
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
