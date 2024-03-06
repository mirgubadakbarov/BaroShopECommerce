using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Size
{
    public class SizeUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

    }
}
