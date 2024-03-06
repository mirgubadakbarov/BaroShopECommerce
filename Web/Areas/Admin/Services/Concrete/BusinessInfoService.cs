using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.BusinessInfo;

namespace Web.Areas.Admin.Services.Concrete
{
    public class BusinessInfoService : IBusinessInfoService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IBusinessInfoRepository _businessInfoRepository;
        private readonly IFileService _fileService;

        public BusinessInfoService(IActionContextAccessor actionContextAccessor,
            IBusinessInfoRepository businessInfoRepository,
            IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _businessInfoRepository = businessInfoRepository;
            _fileService = fileService;
        }
        public async Task<bool> CreateAsync(BusinessInfoCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var maxSize = 2000;
            if (!_fileService.CheckPhoto(model.Photo))
            {
                _modelState.AddModelError("Photo", "File must be image");
                return false;
            }
            else if (!_fileService.MaxSize(model.Photo, maxSize))
            {
                _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb");
                return false;
            }

            var businessInfo = new BusinessInfo
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
            };
            await _businessInfoRepository.CreateAsync(businessInfo);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var businessInfo = await _businessInfoRepository.GetAsync(id);
            await _businessInfoRepository.DeleteAsync(businessInfo);
        }

        public async Task<BusinessInfoIndexVM> GetAllAsync()
        {
            var businessInfo = await _businessInfoRepository.GetAllAsync();
            var model = new BusinessInfoIndexVM
            {
                BusinessInfos = businessInfo,
            };
            return model;

        }

        public async Task<BusinessInfoUpdateVM> GetUpdateModelAsync(int id)
        {
            var businessInfo = await _businessInfoRepository.GetAsync(id);
            var model = new BusinessInfoUpdateVM
            {
                Description = businessInfo.Description,
                Id = businessInfo.Id,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(BusinessInfoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var businessInfo = await _businessInfoRepository.GetAsync(model.Id);
            businessInfo.Description = model.Description;
            businessInfo.ModifiedAt = DateTime.Now;

            if (model.Photo != null)
            {
                var maxSize = 2000;
                if (!_fileService.CheckPhoto(model.Photo))
                {
                    _modelState.AddModelError("Photo", "File must be image");
                    return false;
                }
                else if (!_fileService.MaxSize(model.Photo, maxSize))
                {
                    _modelState.AddModelError("Photo", $"Photo size must be less than {maxSize} kb");
                    return false;
                }
                _fileService.Delete(businessInfo.PhotoName);
                businessInfo.PhotoName = await _fileService.UploadAsync(model.Photo);

            }
            await _businessInfoRepository.UpdateAsync(businessInfo);
            return true;
        }
    }
}
