using Web.Areas.Admin.ViewModels.Service;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IServiceService
    {
        Task<ServiceIndexVM> GetAllAsync();
        Task<bool> CreateAsync(ServiceCreateVM model);
        Task<ServiceUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(ServiceUpdateVM model);
        Task DeleteAsync(int id);
    }
}
