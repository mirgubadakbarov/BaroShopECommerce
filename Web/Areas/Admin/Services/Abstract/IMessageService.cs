using Web.Areas.Admin.ViewModels.Message;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IMessageService
    {
        Task<MessageIndexVM> GetAllAsync();
        Task<MessageDetailsVM> GetDetailsAsync(int id);
        Task DeleteAsync(int id);
    }
}
