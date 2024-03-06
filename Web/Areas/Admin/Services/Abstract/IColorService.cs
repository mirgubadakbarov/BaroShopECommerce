using Web.Areas.Admin.ViewModels.Color;
using Web.Areas.Admin.ViewModels.HomeMainSlider;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IColorService
    {
        Task<bool> CreateAsync(ColorCreateVM model);
        Task<bool> UpdateAsync(ColorUpdateVM model);
        Task<ColorUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);
        Task<ColorIndexVM> GetAllAsync();
    }
}
