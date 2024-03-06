using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Services.Abstract;
using Web.ViewModels.Favourite;

namespace Web.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly IFavouriteService _favouriteService;

        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _favouriteService.GetAllAsync();
            return View(model);
           
        }


        [HttpPost]
        public async Task<IActionResult> Add(FavouriteAddVM model)
        {
            var isSucceded = await _favouriteService.FavouriteAddAsync(model);
            if (isSucceded) return Ok();
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _favouriteService.RemoveAsync(id);
            if (isSucceded) return Ok();
            return NotFound();
        }
    }
}
