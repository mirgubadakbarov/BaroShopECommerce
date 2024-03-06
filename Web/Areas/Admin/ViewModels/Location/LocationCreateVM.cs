using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.Location
{
    public class LocationCreateVM
    {
        [Required]
        public string Url { get; set; }
    }
}
