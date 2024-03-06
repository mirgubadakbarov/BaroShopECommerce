using Web.Areas.Admin.ViewModels.Brand;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IBrandService
    {
        Task<bool> CreateAsync(BrandCreateVM model);
        Task<bool> UpdateAsync(BrandUpdateVM model);
        Task<BrandUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);
        Task<BrandIndexVM> GetAllAsync();
    }
}
