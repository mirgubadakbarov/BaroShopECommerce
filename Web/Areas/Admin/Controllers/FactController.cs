using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Fact;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FactController : Controller
    {
        private readonly IFactService _factService;

        public FactController(IFactService factService)
        {
            _factService = factService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _factService.GetAllAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FactCreateVM model)
        {
            var isSucceded = await _factService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "fact");
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _factService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, FactUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _factService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "fact");
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _factService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "fact");
        }
        #endregion
    }
}
