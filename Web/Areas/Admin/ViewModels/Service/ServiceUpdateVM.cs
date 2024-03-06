using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Service
{
    public class ServiceUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Title { get; set; }
        [Required, MaxLength(30)]
        public string SubTitle { get; set; }
        [Required, MaxLength(50)]
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
