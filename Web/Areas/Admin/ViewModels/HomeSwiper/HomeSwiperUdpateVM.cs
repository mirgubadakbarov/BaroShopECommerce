using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.HomeSwiper
{
    public class HomeSwiperUdpateVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }
        [Required]
        [MaxLength(120)]
        public string Description { get; set; }
        public int Order { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
