using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeMainSliderController : Controller
    {
        private readonly IHomeMainSliderService _homeMainSliderService;

        public HomeMainSliderController(IHomeMainSliderService homeMainSliderService)
        {
            _homeMainSliderService = homeMainSliderService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _homeMainSliderService.GetAllAsync();
            return View(model);
        }

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeMainSliderCreateVM model)
        {
            var isSucced = await _homeMainSliderService.CreateAsync(model);
            if (isSucced) return RedirectToAction(nameof(Index), "homemainslider");
            return View(model);
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeMainSliderService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HomeMainSliderUpdateVM model, int id)
        {
            if (id != model.Id) return BadRequest();
            var isSucceded = await _homeMainSliderService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "homemainslider");
            return View(model);
        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _homeMainSliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "homemainslider");
        }
        #endregion
    }
}
