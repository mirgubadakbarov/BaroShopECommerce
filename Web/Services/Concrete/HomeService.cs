using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.ViewModels.HomeMainSlider;
using Web.Services.Abstract;
using Web.ViewModels.Home;

namespace Web.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly ISpecialSliderRepository _specialSliderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IHomeSpecialDayRepository _homeSpecialDayRepository;
        private readonly IHomeSwiperRepository _homeSwiperRepository;
        private readonly IOurServiceRepository _ourServiceRepository;
        private readonly ITestimonialRepository _testimonialRepository;

        public HomeService(IBrandRepository brandRepository,
            IHomeMainSliderRepository homeMainSliderRepository,
            ISpecialSliderRepository specialSliderRepository,
            IProductRepository productRepository,
            IHomeSpecialDayRepository homeSpecialDayRepository,
            IHomeSwiperRepository homeSwiperRepository,
            IOurServiceRepository ourServiceRepository,
            ITestimonialRepository testimonialRepository)
        {
            _brandRepository = brandRepository;
            _homeMainSliderRepository = homeMainSliderRepository;
            _specialSliderRepository = specialSliderRepository;
            _productRepository = productRepository;
            _homeSpecialDayRepository = homeSpecialDayRepository;
            _homeSwiperRepository = homeSwiperRepository;
            _ourServiceRepository = ourServiceRepository;
            _testimonialRepository = testimonialRepository;
        }
        public async Task<HomeIndexVM> GetAllAsync()
        {
            var model = new HomeIndexVM
            {
                Brands = await _brandRepository.GetAllAsync(),
                HomeMainSliders = await _homeMainSliderRepository.GetAllAsync(),
                SpecialSliders = await _specialSliderRepository.GetAllAsync(),
                BestSellingProducts = await _productRepository.GetProductsBestSellingAsync(),
                InSaleProducts = await _productRepository.GetProductsInSaleAsync(),
                HomeSpecialDay = await _homeSpecialDayRepository.GetAsync(),
                HomeSwipers = await _homeSwiperRepository.GetAllAsync(),
                OurServices = await _ourServiceRepository.GetAllAsync(),
                Testimonials = await _testimonialRepository.GetAllAsync(),
            };
            return model;
        }
    }
}
