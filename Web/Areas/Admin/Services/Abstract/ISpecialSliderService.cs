using Web.Areas.Admin.ViewModels.HomeMainSlider;
using Web.Areas.Admin.ViewModels.SpecialSlider;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ISpecialSliderService
    {
        Task<bool> CreateAsync(SpecialSliderCreateVM model);
        Task<bool> UpdateAsync(SpecialSliderUpdateVM model);
        Task<SpecialSliderUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);
        Task<SpecialSliderIndexVM> GetAllAsync();
    }
}
