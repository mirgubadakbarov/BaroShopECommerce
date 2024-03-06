using Core.Entities;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Components;

namespace Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IBasketProductRepository _basketProductRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;

        public HeaderViewComponent(IBasketProductRepository basketProductRepository,
            IHttpContextAccessor httpContextAccessor,
            IProductRepository productRepository)
        {
            _basketProductRepository = basketProductRepository;
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = new HeaderComponentVM
            {
                Count = await _basketProductRepository.GetUserBasketProductsCount(_httpContextAccessor.HttpContext.User),
                Products=await _productRepository.GetAllAsync()

            };
            return View(model);
        }
    }
}
