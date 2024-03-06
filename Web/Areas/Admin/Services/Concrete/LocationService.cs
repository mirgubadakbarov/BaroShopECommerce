using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Location;

namespace Web.Areas.Admin.Services.Concrete
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ModelStateDictionary _modelState;

        public LocationService(ILocationRepository locationRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _locationRepository = locationRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(LocationCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var location = new Core.Entities.Location
            {
                CreatedAt = DateTime.Now,
                Url = model.Url
            };
            await _locationRepository.CreateAsync(location);
            return true;
        }

        public async Task DeleteAsync()
        {
            var location = await _locationRepository.GetAsync();
            await _locationRepository.DeleteAsync(location);
        }

        public async Task<LocationIndexVM> GetAsync()
        {
            var location = await _locationRepository.GetAsync();
            if (location == null) return null;
            var model = new LocationIndexVM
            {
                Id = location.Id,
                Url = location.Url,
                CreatedAt = location.CreatedAt,
                ModifiedAt = location.ModifiedAt,
            };
            return model;
        }

        public async Task<LocationUpdateVM> GetUpdateModelAsync()
        {
            var location = await _locationRepository.GetAsync();
            var model = new LocationUpdateVM
            {
                Url = location.Url,
            };
            return model;
        }

        public async Task<bool> IsExistAsync()
        {
            var isExist = await _locationRepository.GetAsync();
            if (isExist != null) return true;
            return false;
        }

        public async Task<bool> UpdateAsync(LocationUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var location = await _locationRepository.GetAsync();
            location.Url = model.Url;
            location.ModifiedAt = DateTime.Now;

            await _locationRepository.UpdateAsync(location);
            return true;
        }
    }
}
