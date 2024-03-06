using Web.Areas.Admin.ViewModels.Location;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ILocationService
    {
        Task<LocationIndexVM> GetAsync();
        Task<bool> CreateAsync(LocationCreateVM model);
        Task<LocationUpdateVM> GetUpdateModelAsync();
        Task<bool> UpdateAsync(LocationUpdateVM model);
        Task DeleteAsync();
        Task<bool> IsExistAsync();
    }
}
