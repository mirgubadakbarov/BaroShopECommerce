using Core.Entities;
using Web.ViewModels.About;

namespace Web.Services.Abstract
{
    public interface IAboutService
    {
        Task<AboutIndexVM> GetAllAsync();
        Task<bool> SendMessageAsync(SendMessage message);
    }
}
