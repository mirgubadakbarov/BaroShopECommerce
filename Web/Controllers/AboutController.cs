using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _aboutService.GetAllAsync();
            return View(model);
        }

        #region SendMessage
        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessage message)
        {
            //=> RedirectToAction(actionName, controllerName, routeValues, fragment: null);
            var isSucceded = await _aboutService.SendMessageAsync(message);
            if (isSucceded) return RedirectToAction(nameof(Index), "about", null, "contact-info");
            return RedirectToAction(nameof(Index), "about");
        }
        #endregion
    }
}
