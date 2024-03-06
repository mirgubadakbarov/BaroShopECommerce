using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.SpecialSlider;

namespace Web.Areas.Admin.Services.Concrete
{
    public class SpecialSliderService : ISpecialSliderService
    {
        private readonly ISpecialSliderRepository _specialSliderRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public SpecialSliderService(ISpecialSliderRepository specialSliderRepository,
            IFileService fileService,
            IActionContextAccessor actionContextAccessor)
        {
            _specialSliderRepository = specialSliderRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(SpecialSliderCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _specialSliderRepository.AnyAsync(ss => ss.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This slider already created");
                return false;
            }
            var maxSize = 5000;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "File must be image");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb;");
                return false;
            }
            var count = await _specialSliderRepository.GetAllAsync();
            int order = count.Count();
            if (order == 0)
            {
                order = 1;
            }
            else
            {
                order++;
            }
            var specialSlider = new SpecialSlider
            {
                ButtonLink = model.ButtonLink,
                CreatedAt = DateTime.Now,
                Description = model.Description,
                Title = model.Title,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                Order = order,
            };

            await _specialSliderRepository.CreateAsync(specialSlider);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var specialSlide = await _specialSliderRepository.GetAsync(id);
            await _specialSliderRepository.DeleteAsync(specialSlide);
        }

        public async Task<SpecialSliderIndexVM> GetAllAsync()
        {
            var model = new SpecialSliderIndexVM
            {
                SpecialSliders = await _specialSliderRepository.GetAllAsync(),
            };
            return model;
        }

        public async Task<SpecialSliderUpdateVM> GetUpdateModelAsync(int id)
        {
            var specialSlider = await _specialSliderRepository.GetAsync(id);
            var model = new SpecialSliderUpdateVM
            {
                ButtonLink = specialSlider.ButtonLink,
                Description = specialSlider.Description,
                Order = specialSlider.Order,
                Title = specialSlider.Title,
                Id = specialSlider.Id,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(SpecialSliderUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _specialSliderRepository.AnyAsync(ss => ss.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && ss.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This slider already created");
                return false;
            }
            var specialSlider = await _specialSliderRepository.GetAsync(model.Id);
            specialSlider.ButtonLink = model.ButtonLink;
            specialSlider.Description = model.Description;
            specialSlider.Title = model.Title;
            specialSlider.ModifiedAt = DateTime.Now;

            if (model.Order != 0)
            {
                specialSlider.Order = model.Order;
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
                _fileService.Delete(specialSlider.PhotoName);
                specialSlider.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _specialSliderRepository.UpdateAsync(specialSlider);
            return true;
        }
    }
}
