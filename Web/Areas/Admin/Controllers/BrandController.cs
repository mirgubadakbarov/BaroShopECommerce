using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Brand;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _brandService.GetAllAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateVM model)
        {
            var isSucceded = await _brandService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "brand");
            return View(model);
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _brandService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandUpdateVM model, int id)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _brandService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "brand");
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "brand");
        }
        #endregion
    }
}
