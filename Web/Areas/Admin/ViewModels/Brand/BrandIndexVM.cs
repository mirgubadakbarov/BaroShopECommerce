using Core.Entities;

namespace Web.Areas.Admin.ViewModels.Brand
{
    public class BrandIndexVM
    {
        public BrandIndexVM()
        {
            Brands = new List<Core.Entities.Brand>();
        }
        public List<Core.Entities.Brand> Brands { get; set; }

    }
}
