using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeSpecialDay;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeSpecialDayService : IHomeSpecialDayService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IHomeSpecialDayRepository _homeSpecialDayRepository;
        private readonly IFileService _fileService;

        public HomeSpecialDayService(IActionContextAccessor actionContextAccessor,
            IHomeSpecialDayRepository homeSpecialDayRepository,
            IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _homeSpecialDayRepository = homeSpecialDayRepository;
            _fileService = fileService;
        }
        public async Task<bool> CreateAsync(HomeSpecialDayCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _homeSpecialDayRepository.GetAllAsync();
            if (isExist.Count > 0)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }
            var maxSize = 5500;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "File must be image format");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb");
                return false;
            }

            var homeSpecialDay = new HomeSpecialDay
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                Title = model.Title,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                Day = model.Day,
                Hour = model.Hour,
                Minute = model.Minute,
                Month = model.Month,
                Year = model.Year,
            };

            await _homeSpecialDayRepository.CreateAsync(homeSpecialDay);
            return true;

        }

        public async Task DeleteAsync(int id)
        {
            var homeSpecialDay = await _homeSpecialDayRepository.GetAsync(id);
            await _homeSpecialDayRepository.DeleteAsync(homeSpecialDay);
        }

        public async Task<HomeSpecialDayIndexVM> GetHomeSpecialDayIndexAsync()
        {
            var homeSpecial = await _homeSpecialDayRepository.GetHomeSpecialDay();

            var model = new HomeSpecialDayIndexVM
            {
                HomeSpecialDay = homeSpecial
            };
            return model;
        }

        public async Task<HomeSpecialDayUpdateVM> GetUpdataModelAsync(int id)
        {
            var homeSpecialDay = await _homeSpecialDayRepository.GetAsync(id);
            if (homeSpecialDay == null) return null;

            var model = new HomeSpecialDayUpdateVM
            {
                Day = homeSpecialDay.Day,
                Hour = homeSpecialDay.Hour,
                Minute = homeSpecialDay.Minute,
                Month = homeSpecialDay.Month,
                Year = homeSpecialDay.Year,
                Description = homeSpecialDay.Description,
                Title = homeSpecialDay.Title,
                Id = id,
            };
            return model;
        }

        public async Task<bool> IsExistAsync()
        {
            var isExist = await _homeSpecialDayRepository.GetAsync();
            if (isExist != null) return true;
            return false;
        }

        public async Task<bool> UpdateAsync(HomeSpecialDayUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var homeSpecialDay = await _homeSpecialDayRepository.GetAsync(model.Id);
            homeSpecialDay.Description = model.Description;
            homeSpecialDay.Title = model.Title;
            homeSpecialDay.ModifiedAt = DateTime.Now;
            homeSpecialDay.Day = model.Day;
            homeSpecialDay.Hour = model.Hour;
            homeSpecialDay.Month = model.Month;
            homeSpecialDay.Year = model.Year;
            homeSpecialDay.Minute = model.Minute;
            if (model.Photo != null)
            {
                var maxSize = 5500;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "File format must be image");
                    return false;
                }
                else if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb;");
                    return false;

                }
                _fileService.Delete(homeSpecialDay.PhotoName);
                homeSpecialDay.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _homeSpecialDayRepository.UpdateAsync(homeSpecialDay);
            return true;
        }
    }
}
