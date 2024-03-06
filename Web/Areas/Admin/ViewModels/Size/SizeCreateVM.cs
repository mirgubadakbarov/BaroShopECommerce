using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Size
{
    public class SizeCreateVM
    {
        [Required]
        public string Title { get; set; }
    }
}
