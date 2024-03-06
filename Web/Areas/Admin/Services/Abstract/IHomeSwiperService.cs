using Web.Areas.Admin.ViewModels.HomeSwiper;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IHomeSwiperService
    {
        Task<HomeSwiperIndexVM> GetAllAsync();
        Task<bool> CreateAsync(HomeSwiperCreateVM model);
        Task<HomeSwiperUdpateVM> GetUpdateModelAsync(int id);
        Task<bool> UdpateAsync(HomeSwiperUdpateVM model);
        Task DeleteAsync(int id);


    }
}
