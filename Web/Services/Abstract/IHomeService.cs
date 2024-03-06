using Web.Areas.Admin.ViewModels.HomeMainSlider;
using Web.ViewModels.Home;

namespace Web.Services.Abstract
{
    public interface IHomeService
    {
        Task<HomeIndexVM> GetAllAsync();
    }
}
