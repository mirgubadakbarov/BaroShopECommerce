using Web.Areas.Admin.ViewModels.BusinessInfo;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IBusinessInfoService
    {
        Task<BusinessInfoIndexVM> GetAllAsync();
        Task<BusinessInfoUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(BusinessInfoUpdateVM model);

        Task<bool> CreateAsync(BusinessInfoCreateVM model);
        Task DeleteAsync(int id);
    }
}
