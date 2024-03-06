using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.WhatWeDo
{
    public class WhatWeDoUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
