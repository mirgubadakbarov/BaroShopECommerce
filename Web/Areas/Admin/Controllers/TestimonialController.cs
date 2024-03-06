using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Testimonial;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _testimonialService.GetAllAsync();
            return View(model);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TestimonialCreateVM model)
        {
            var isSucceded = await _testimonialService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "testimonial");
            return View(model);
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _testimonialService.GetUpdateModelAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TestimonialUpdateVM model)
        {
            if (id != model.Id) return BadRequest();
            var isSucceded = await _testimonialService.UpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _testimonialService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "testimonial");
        }
        #endregion


    }
}
