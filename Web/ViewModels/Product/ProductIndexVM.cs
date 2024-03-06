using Core.Constants;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.ViewModels.Product
{
    public class ProductIndexVM
    {
        public List<Core.Entities.Product>? Products { get; set; }
        public List<Brand>? Brands { get; set; }
        public List<Color>? Colors { get; set; }
        public List<Gender>? Genders { get; set; }
        public List<Model>? Models { get; set; }
        public List<Material>? Materials { get; set; }
        public int Page { get; set; } = 1;
        public int Take { get; set; } = 12;
        public int PageCount { get; set; }
        public string? SearchInput { get; set; }

        [Display(Name = "Min.Price")]
        public double? MinPrice { get; set; }
        [Display(Name = "Max.Price")]
        public double? MaxPrice { get; set; }
        public string? Gender { get; set; }
        public string Model { get; set; }
        public string Material { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }

    }
}
