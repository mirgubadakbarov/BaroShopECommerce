using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.BusinessInfo;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BusinessInfoController : Controller
    {
        private readonly IBusinessInfoService _businessInfoService;

        public BusinessInfoController(IBusinessInfoService businessInfoService)
        {
            _businessInfoService = businessInfoService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _businessInfoService.GetAllAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BusinessInfoCreateVM model)
        {
            var isSucceded = await _businessInfoService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "businessinfo");
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _businessInfoService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BusinessInfoUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _businessInfoService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "businessinfo");
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _businessInfoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "businessinfo");
        }
        #endregion
    }
}
