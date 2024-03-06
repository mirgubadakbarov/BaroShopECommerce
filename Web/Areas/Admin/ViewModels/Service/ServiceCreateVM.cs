using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Web.Areas.Admin.ViewModels.Service
{
    public class ServiceCreateVM
    {
        [Required, MaxLength(30)]
        public string Title { get; set; }
        [Required, MaxLength(30)]
        public string SubTitle { get; set; }
        [Required,MaxLength(50)]

        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
