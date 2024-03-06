using Core.Entities;

namespace Web.ViewModels.Home
{
    public class HomeIndexVM
    {
        public List<HomeMainSlider> HomeMainSliders { get; set; }
        public List<Brand> Brands { get; set; }
        public List<SpecialSlider> SpecialSliders { get; set; }
        public List<Core.Entities.Product> BestSellingProducts { get; set; }
        public List<Core.Entities.Product> InSaleProducts { get; set; }
        public HomeSpecialDay HomeSpecialDay { get; set; }
        public List<HomeSwiper> HomeSwipers { get; set; }
        public List<OurService> OurServices { get; set; }
        public List<Testimonial> Testimonials { get; set; }




    }
}
