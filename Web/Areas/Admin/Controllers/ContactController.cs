using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Contact;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _contactService.GetAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var isExist = await _contactService.GetAsync();
            if (isExist != null) return NotFound();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateVM model)
        {
            var isSucceded = await _contactService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var model = await _contactService.GetUpdateModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ContactUpdateVM model)
        {
            var isSucceded = await _contactService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            await _contactService.DeleteAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
