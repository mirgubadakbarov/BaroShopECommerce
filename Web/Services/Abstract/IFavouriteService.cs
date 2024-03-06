using Web.ViewModels.Favourite;

namespace Web.Services.Abstract
{
    public interface IFavouriteService
    {
        Task<bool> FavouriteAddAsync(FavouriteAddVM model);
        Task<List<FavouriteListItemVM>> GetAllAsync();

        Task<bool> RemoveAsync(int id);

    }
}
