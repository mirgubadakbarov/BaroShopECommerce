using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeSpecialDay;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeSpecialDayController : Controller
    {
        private readonly IHomeSpecialDayService _homeSpecialDayService;

        public HomeSpecialDayController(IHomeSpecialDayService homeSpecialDayService)
        {
            _homeSpecialDayService = homeSpecialDayService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _homeSpecialDayService.GetHomeSpecialDayIndexAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var isExist=await  _homeSpecialDayService.IsExistAsync();
            if (isExist) return NotFound();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HomeSpecialDayCreateVM model)
        {
            var isSucceded = await _homeSpecialDayService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeSpecialDayService.GetUpdataModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeSpecialDayUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _homeSpecialDayService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "homespecialday");
            return View(model);
        }


        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _homeSpecialDayService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "homespecialday");
        }
        #endregion
    }
}
