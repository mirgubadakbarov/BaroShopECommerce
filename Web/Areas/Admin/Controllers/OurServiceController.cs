using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurService;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OurServiceController : Controller
    {
        private readonly IOurServiceService _ourServiceService;

        public OurServiceController(IOurServiceService ourServiceService)
        {
            _ourServiceService = ourServiceService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _ourServiceService.GetAllAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OurServiceCreateVM model)
        {
            var isSucceded=await _ourServiceService.CreateAsync(model);
            if(isSucceded)return RedirectToAction(nameof(Index),"ourservice");
            return View(model);
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model=await _ourServiceService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,OurServiceUpdateVM model)
        {
            if (model.Id != id) return BadRequest();
            var isSucceded = await _ourServiceService.UpdateAsync(model);
            if(isSucceded) return RedirectToAction(nameof(Index),"ourservice");
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _ourServiceService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "ourservice");
        }
        #endregion

    }
}
