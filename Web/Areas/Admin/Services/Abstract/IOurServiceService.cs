using Web.Areas.Admin.ViewModels.OurService;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IOurServiceService
    {
        Task<OurServiceIndexVM> GetAllAsync();
        Task<bool> CreateAsync(OurServiceCreateVM model);
        Task<OurServiceUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(OurServiceUpdateVM model);
        Task DeleteAsync(int id);
    }
}
