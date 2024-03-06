using Web.Areas.Admin.ViewModels.Fact;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IFactService
    {
        Task<FactIndexVM> GetAllAsync();
        Task<bool> CreateAsync(FactCreateVM model);
        Task<FactUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(FactUpdateVM model);

        Task DeleteAsync(int id);
    }
}
