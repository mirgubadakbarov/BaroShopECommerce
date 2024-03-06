using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.WhatWeDo;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WhatWeDoController : Controller
    {
        private readonly IWhatWeDoService _whatWeDoService;

        public WhatWeDoController(IWhatWeDoService whatWeDoService)
        {
            _whatWeDoService = whatWeDoService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _whatWeDoService.GetAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var isExist = await _whatWeDoService.GetAsync();
            if (isExist.WhatWedo!=null) return NotFound();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WhatWeDoCreateVM model)
        {
            var isSucceded = await _whatWeDoService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "whatwedo");
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var model = await _whatWeDoService.GetUpdateModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WhatWeDoUpdateVM model)
        {
            var isSucceded = await _whatWeDoService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "whatwedo");
            return View();
        }
        #endregion

        #region Delete 
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _whatWeDoService.DeleteAsync();
            return RedirectToAction(nameof(Index), "whatwedo");

        }
        #endregion
    }
}
