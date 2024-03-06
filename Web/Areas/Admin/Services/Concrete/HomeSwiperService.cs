using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.HomeSwiper;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeSwiperService : IHomeSwiperService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IHomeSwiperRepository _homeSwiperRepository;
        private readonly IFileService _fileService;

        public HomeSwiperService(IActionContextAccessor actionContextAccessor,
            IHomeSwiperRepository homeSwiperRepository,
            IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _homeSwiperRepository = homeSwiperRepository;
            _fileService = fileService;
        }
        public async Task<bool> CreateAsync(HomeSwiperCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _homeSwiperRepository.AnyAsync(hs => hs.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
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
                _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb;");
                return false;
            }
            var count = await _homeSwiperRepository.GetAllAsync();
            var order = count.Count();
            if (order == 0)
            {
                order = 1;
            }
            var homeSwiper = new HomeSwiper
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                Title = model.Title,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                Order = order,

            };
            await _homeSwiperRepository.CreateAsync(homeSwiper);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var homeSwiper = await _homeSwiperRepository.GetAsync(id);
            await _homeSwiperRepository.DeleteAsync(homeSwiper);
        }

        public async Task<HomeSwiperIndexVM> GetAllAsync()
        {
            var homeSwiper = await _homeSwiperRepository.GetAllAsync();
            var model = new HomeSwiperIndexVM
            {
                HomeSwipers = homeSwiper,
            };
            return model;
        }

        public async Task<HomeSwiperUdpateVM> GetUpdateModelAsync(int id)
        {
            var updateModel = await _homeSwiperRepository.GetAsync(id);
            var model = new HomeSwiperUdpateVM
            {
                Description = updateModel.Description,
                Id = id,
                Order = updateModel.Order,
                Title = updateModel.Title,
            };
            return model;
        }

        public async Task<bool> UdpateAsync(HomeSwiperUdpateVM model)
        {
            if (!_modelState.IsValid) return false;
            var homeSwiper = await _homeSwiperRepository.GetAsync(model.Id);
            homeSwiper.Description = model.Description;
            homeSwiper.Title = model.Title;
            homeSwiper.Order = model.Order;
            homeSwiper.ModifiedAt = DateTime.Now;

            if (model.Photo != null)
            {
                var maxSize = 5500;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "File must be image format");
                    return false;
                }
                else if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb;");
                    return false;
                }
                _fileService.Delete(homeSwiper.PhotoName);
                homeSwiper.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _homeSwiperRepository.UpdateAsync(homeSwiper);
            return true;
        }
    }
}
