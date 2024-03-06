using Web.Areas.Admin.ViewModels.Contact;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IContactService
    {
        Task<ContactIndexVM> GetAsync();
        Task<bool> CreateAsync(ContactCreateVM model);
        Task<ContactUpdateVM> GetUpdateModelAsync();
        Task<bool> UpdateAsync(ContactUpdateVM model);
        Task DeleteAsync();
    }
}
