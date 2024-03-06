using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Location;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _locationService.GetAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var isExist = await _locationService.IsExistAsync();
            if (isExist) return NotFound();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationCreateVM model)
        {
            var isSucceded = await _locationService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var model = await _locationService.GetUpdateModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LocationUpdateVM model)
        {
            var isSucceded = await _locationService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion


        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _locationService.DeleteAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
