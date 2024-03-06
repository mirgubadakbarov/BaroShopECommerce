using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductAddColorVM
    {
        public int ProductId { get; set; }
        [Display(Name = "Colors")]
        [Required]
        public List<int> ColorsIds { get; set; }
        public List<SelectListItem>? Colors { get; set; }
    }
}
