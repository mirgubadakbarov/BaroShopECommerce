using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1200)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public ShippingStatus? ShippingStatus { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<ProductPhoto>? ProductPhotos { get; set; }

        public Gender Gender { get; set; }
        public Material Material { get; set; }
        public Model Model { get; set; }
        public bool BestSelling { get; set; }
        public bool InSale { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public List<SelectListItem>? Brands { get; set; }
        [Display(Name = "Colors")]
        public List<int>? ColorsIds { get; set; }
        public List<SelectListItem>? Colors { get; set; }
        [Display(Name = "Sizes")]
        public List<int>? SizesIds { get; set; }
        public List<SelectListItem>? Sizes { get; set; }

        public List<ProductColor>? CurrentColors { get; set; }
        public List<ProductSize>? CurrentSizes { get; set; }


    }
}
