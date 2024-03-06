using Core.Constants;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web.Services.Abstract;
using Web.ViewModels.Components;
using Web.ViewModels.Product;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index(ProductIndexVM model)
        {
            model = await _productService.GetAllAsync(model);
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _productService.GetDetailsAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMore(int skipRow)
        {
            var model = await _productService.GetLoadMoreAsync(skipRow);
            return PartialView("_BestSellingPartial", model);
        }


        [HttpGet]
        public async Task<IActionResult> FilterByName(string? name)
        {
            var model = await _productService.FilterAllByName(name);
            return PartialView("_SearchProductPartial", model);
        }
    }
}
