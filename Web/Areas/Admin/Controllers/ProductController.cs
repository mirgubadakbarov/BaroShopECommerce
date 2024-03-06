using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _productService.GetAllAsync();
            return View(model);
        }


        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _productService.GetProductDetailsAsync(id);
            return View(model);
        }
        #endregion


        #region  Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _productService.GetBrandCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM product)
        {
            var isSucceded = await _productService.CreateAsync(product);
            if (isSucceded) return RedirectToAction(nameof(Index), "product");
            return View(product);
        }

        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _productService.GetUpdateModelAsync(id);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductUpdateVM product)
        {
            if (product.Id != id) return BadRequest();
            var isSucceded = await _productService.UpdateAsync(product);
            if (isSucceded) return RedirectToAction(nameof(Index), "product");
            return View(product);
        }
        #endregion


        #region Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), "product");
        }
        #endregion


        #region AddColor

        [HttpGet]
        public async Task<IActionResult> AddColor(int id)
        {
            var colors = await _productService.GetColorAddModelAsync();
            return View(colors);
        }

        [HttpPost]
        public async Task<IActionResult> AddColor(ProductAddColorVM model, int id)
        {
            var isSucceded = await _productService.AddColorAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "product");
            return View(model);
        }

        #endregion


        #region AddSize

        [HttpGet]
        public async Task<IActionResult> AddSize(int id)
        {
            var sizes = await _productService.GetSizeAddModelAsync();
            return View(sizes);
        }

        [HttpPost]
        public async Task<IActionResult> AddSize(ProductAddSizeVM model, int id)
        {
            var isSucceded = await _productService.AddSizeAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "product");
            return View(model);
        }
        #endregion

        #region UpdateSize
        [HttpPost]
        public async Task<IActionResult> UpdateSize(int id)
        {
            return View();
        }
        #endregion

        #region UpdatePhoto
        [HttpGet]
        public async Task<IActionResult> UpdatePhoto(int id)
        {
            var model = await _productService.GetPhotoUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePhoto(int id, ProductPhotoUpdateVM model)
        {
            if (id != model.Id) return BadRequest();
            var isSucceded = await _productService.PhotoUpdateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Update), "product", new { id = model.ProductId });
            return View(model);
        }
        #endregion


        #region DeletePhoto
        [HttpPost]
        public async Task<IActionResult> DeletePhoto(int id, int productId)
        {
            await _productService.DeletePhotoAsync(id);
            return RedirectToAction(nameof(Update), "product", new { id = productId });
        }


        #endregion
    }
}
