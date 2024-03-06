using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels.Favourite
{
    public class FavouriteListItemVM
    {
        public int Id { get; set; }

        public double Price { get; set; }
        public string Title { get; set; }
        public string PhotoName { get; set; }
        public int SizeId { get; set; }
        public List<SelectListItem> Sizes { get; set; }
        public ShippingStatus? ShippingStatus { get; set; }
    }
}
