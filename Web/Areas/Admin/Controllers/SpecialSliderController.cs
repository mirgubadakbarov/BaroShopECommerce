using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.SpecialSlider;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SpecialSliderController : Controller
    {
        private readonly ISpecialSliderService _specialSliderService;

        public SpecialSliderController(ISpecialSliderService specialSliderService)
        {
            _specialSliderService = specialSliderService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _specialSliderService.GetAllAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecialSliderCreateVM model)
        {
            var isSucceded = await _specialSliderService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "specialslider");
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _specialSliderService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SpecialSliderUpdateVM model, int id)
        {
            if (id != model.Id) return BadRequest();
            var isSucceded = await _specialSliderService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "specialslider");
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _specialSliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "specialslider");
        }
        #endregion
    }
}
