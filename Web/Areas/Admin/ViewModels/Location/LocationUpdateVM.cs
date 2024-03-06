using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.Location
{
    public class LocationUpdateVM
    {
        [Required]
        public string Url { get; set; }
    }
}
