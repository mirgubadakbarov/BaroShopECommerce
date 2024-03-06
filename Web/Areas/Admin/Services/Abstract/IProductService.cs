using Core.Entities;
using DataAccess.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.ViewModels.HomeMainSlider;
using Web.Areas.Admin.ViewModels.Product;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductCreateVM model);
        Task<bool> UpdateAsync(ProductUpdateVM model);
        Task<ProductUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);
        Task<ProductIndexVM> GetAllAsync();
        Task<List<SelectListItem>> GetBrandSelectListAsync();
        Task<ProductCreateVM> GetBrandCreateModelAsync();
        Task<ProductAddColorVM> GetColorAddModelAsync();
        Task<List<SelectListItem>> GetColorsSelectAsync();
        Task<List<SelectListItem>> GetSizesSelectAsync();

        Task<bool> AddColorAsync(ProductAddColorVM model);
        Task<bool> AddSizeAsync(ProductAddSizeVM model);
        Task<ProductAddSizeVM> GetSizeAddModelAsync();

        Task<ProductDetailsVM> GetProductDetailsAsync(int id);

        Task<ProductPhotoUpdateVM> GetPhotoUpdateModelAsync(int id);
        Task<bool> PhotoUpdateAsync(ProductPhotoUpdateVM model);
        Task DeletePhotoAsync(int id);







    }
}
