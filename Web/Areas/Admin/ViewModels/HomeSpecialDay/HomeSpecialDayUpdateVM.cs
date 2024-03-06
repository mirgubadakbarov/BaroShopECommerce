using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.HomeSpecialDay
{
    public class HomeSpecialDayUpdateVM
    {


        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(60)]
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
