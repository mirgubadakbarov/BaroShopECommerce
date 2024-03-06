using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Size;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SizeController : Controller
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _sizeService.GetAllAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SizeCreateVM model)
        {
            var isSucceded = await _sizeService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "size");
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _sizeService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SizeUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _sizeService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "size");
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _sizeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "size");
        }
        #endregion
    }
}
