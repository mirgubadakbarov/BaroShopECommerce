using Core.Constants;
using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ShippingStatus? ShippingStatus { get; set; }
        public Gender Gender { get; set; }
        public Material Material { get; set; }
        public Model Model { get; set; }
        public string MainPhoto { get; set; }
        public double Price { get; set; }
        public List<ProductPhoto> ProductPhotos { get; set; }
        public ICollection<ProductColor>? Colors { get; set; }
        public ICollection<ProductSize>? Sizes { get; set; }
        public bool BestSelling { get; set; }
        public bool InSale { get; set; }
        public ICollection<BasketProduct> BasketProducts { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }





    }
}
