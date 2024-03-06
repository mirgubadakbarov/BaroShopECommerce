using Web.Areas.Admin.ViewModels.Color;
using Web.Areas.Admin.ViewModels.Size;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ISizeService
    {
        Task<bool> CreateAsync(SizeCreateVM model);
        Task<bool> UpdateAsync(SizeUpdateVM model);
        Task<SizeUpdateVM> GetUpdateModelAsync(int id);
        Task DeleteAsync(int id);
        Task<SizeIndexVM> GetAllAsync();
    }
}
