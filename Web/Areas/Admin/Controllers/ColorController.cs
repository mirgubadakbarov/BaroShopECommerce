using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Color;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;
        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _colorService.GetAllAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ColorCreateVM model)
        {
            var isSucceded = await _colorService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "color");
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _colorService.GetUpdateModelAsync(id);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Update(ColorUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _colorService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "color");
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _colorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "color");
        }
        #endregion
    }
}
