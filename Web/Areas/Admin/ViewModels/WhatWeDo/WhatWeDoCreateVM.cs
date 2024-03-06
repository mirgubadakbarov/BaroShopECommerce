using Microsoft.Build.Framework;

namespace Web.Areas.Admin.ViewModels.WhatWeDo
{
    public class WhatWeDoCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }

    }
}
