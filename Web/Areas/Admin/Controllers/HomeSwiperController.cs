using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeSwiper;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeSwiperController : Controller
    {
        private readonly IHomeSwiperService _homeSwiperService;

        public HomeSwiperController(IHomeSwiperService homeSwiperService)
        {
            _homeSwiperService = homeSwiperService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _homeSwiperService.GetAllAsync();
            return View(model);
        }


        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeSwiperCreateVM model)
        {
            var isSucceded = await _homeSwiperService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "homeswiper");
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeSwiperService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeSwiperUdpateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _homeSwiperService.UdpateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "homeswiper");
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _homeSwiperService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "homeswiper");
        }
        #endregion

    }
}
