using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Web.Services.Abstract;
using Web.ViewModels.Favourite;

namespace Web.Services.Concrete
{
    public class FavouriteService : IFavouriteService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly ISizeRepository _sizeRepository;

        public FavouriteService(IHttpContextAccessor httpContextAccessor,
            IProductRepository productRepository,
            IProductSizeRepository productSizeRepository,
            ISizeRepository sizeRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            _productSizeRepository = productSizeRepository;
            _sizeRepository = sizeRepository;
        }
        public async Task<bool> FavouriteAddAsync(FavouriteAddVM model)
        {
            List<FavouriteAddVM> favourite;
            if (_httpContextAccessor.HttpContext.Request.Cookies["favourite"] != null)
            {
                favourite = JsonConvert.DeserializeObject<List<FavouriteAddVM>>(_httpContextAccessor.HttpContext.Request.Cookies["favourite"]);
            }
            else
            {
                favourite = new List<FavouriteAddVM>();
            }

            var favouriteProduct = favourite.Find(f => f.Id == model.Id);
            if (favouriteProduct == null)
            {
                favourite.Add(model);
                var serializedItem = JsonConvert.SerializeObject(favourite);

                _httpContextAccessor.HttpContext.Response.Cookies.Append("favourite", serializedItem);
                return true;
            }

            return false;

        }

        public async Task<List<FavouriteListItemVM>> GetAllAsync()
        {
            List<FavouriteAddVM> favourites;
            if (_httpContextAccessor.HttpContext.Request.Cookies["favourite"] != null)
            {
                favourites = JsonConvert.DeserializeObject<List<FavouriteAddVM>>(_httpContextAccessor.HttpContext.Request.Cookies["favourite"]);
                List<FavouriteListItemVM> model = new List<FavouriteListItemVM>();

                foreach (var favourite in favourites)
                {
                    var dbitem = await _productRepository.GetProductDetailsAsync(favourite.Id);
                    if (dbitem != null)
                    {
                        var size = await _sizeRepository.GetAllAsync();
                        model.Add(new FavouriteListItemVM
                        {
                            Id = dbitem.Id,
                            PhotoName = dbitem.MainPhoto,
                            Price = dbitem.Price,
                            ShippingStatus = dbitem.ShippingStatus,
                            Sizes = size.Select(s => new SelectListItem
                            {
                                Text = s.Title,
                                Value = s.Id.ToString(),

                            }).ToList(),
                            Title = dbitem.Title
                        });
                    }
                }

                return model;
            }
            return null;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            List<FavouriteAddVM> favourite;
            if (_httpContextAccessor.HttpContext.Request.Cookies["favourite"] == null) return false;
            favourite = JsonConvert.DeserializeObject<List<FavouriteAddVM>>(_httpContextAccessor.HttpContext.Request.Cookies["favourite"]);

            foreach (var item in favourite)
            {
                if (item.Id == id)
                {
                    favourite.Remove(item);
                    var serializedItem = JsonConvert.SerializeObject(favourite);
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("favourite", serializedItem);
                    return true;
                }
            }


            return true;

        }
    }
}
