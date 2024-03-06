using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeMainSlider;


namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeMainSliderService : IHomeMainSliderService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public HomeMainSliderService(IHomeMainSliderRepository homeMainSliderRepository,
            IActionContextAccessor actionContextAccessor,
            IFileService fileService)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(HomeMainSliderCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _homeMainSliderRepository.AnyAsync(ms => ms.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Slider already created");
                return false;
            }
            int maxSize = 5000;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "File must be photo");
                return false;
            }
            if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb;");
                return false;
            }
            var count = await _homeMainSliderRepository.GetAllAsync();
            int order = count.Count();
            if (order == 0)
            {
                order = 1;
            }
            else
            {
                order++;
            }
            var homeMainSlider = new HomeMainSlider
            {
                Title = model.Title,
                Subtitle = model.Subtitle,
                ButtonLink = model.ButtonLink,
                CreatedAt = DateTime.Now,
                Order = order,
                PhotoName = await _fileService.UploadAsync(model.Photo),
            };

            await _homeMainSliderRepository.CreateAsync(homeMainSlider);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var homeSlider = await _homeMainSliderRepository.GetAsync(id);
            if (homeSlider != null) await _homeMainSliderRepository.DeleteAsync(homeSlider);
        }

        public async Task<HomeMainSliderIndexVM> GetAllAsync()
        {
            var model = new HomeMainSliderIndexVM { HomeMainSliders = await _homeMainSliderRepository.GetAllAsync() };
            return model;
        }

        public async Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id)
        {
            var homeSlider = await _homeMainSliderRepository.GetAsync(id);

            var model = new HomeMainSliderUpdateVM
            {
                Id = homeSlider.Id,
                Title = homeSlider.Title,
                Subtitle = homeSlider.Subtitle,
                ButtonLink = homeSlider.ButtonLink,
                Order = homeSlider.Order,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(HomeMainSliderUpdateVM model)
        {
            var updateSlider = await _homeMainSliderRepository.GetAsync(model.Id);
            var isExist = await _homeMainSliderRepository.AnyAsync(ms => ms.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && ms.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Slider already created");
                return false;
            }
            updateSlider.Subtitle = model.Subtitle;
            updateSlider.Title = model.Title;
            updateSlider.ModifiedAt = DateTime.Now;
            updateSlider.ButtonLink = model.ButtonLink;

            if (model.Order != 0)
            {
                updateSlider.Order = model.Order;
            }
            if (model.Photo != null)
            {
                int maxSize = 5000;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "File must be photo");
                    return false;
                }
                if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb;");
                    return false;
                }
                _fileService.Delete(updateSlider.PhotoName);
                updateSlider.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _homeMainSliderRepository.UpdateAsync(updateSlider);
            return true;
        }
    }
}
