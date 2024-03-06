using Web.Areas.Admin.ViewModels.HomeSpecialDay;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeSpecialDayService
    {
        Task<HomeSpecialDayIndexVM> GetHomeSpecialDayIndexAsync();
        Task<bool> CreateAsync(HomeSpecialDayCreateVM model);
        Task<HomeSpecialDayUpdateVM> GetUpdataModelAsync(int id);
        Task<bool> UpdateAsync(HomeSpecialDayUpdateVM model);
        Task DeleteAsync(int id);
        Task<bool> IsExistAsync();
    }
}
