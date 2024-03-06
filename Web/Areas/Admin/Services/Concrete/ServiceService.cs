using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Service;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IFileService _fileService;
        private readonly ModelStateDictionary _modelState;

        public ServiceService(IServiceRepository serviceRepository,
            IActionContextAccessor actionContextAccessor,
            IFileService fileService)
        {
            _serviceRepository = serviceRepository;
            _fileService = fileService;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(ServiceCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _serviceRepository.AnyAsync(s => s.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }
            var maxSize = 2000;
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

            var service = new Service
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                SubTitle = model.SubTitle,
                Title = model.Title,
                PhotoName = await _fileService.UploadAsync(model.Photo),
            };
            await _serviceRepository.CreateAsync(service);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _serviceRepository.GetAsync(id);
            await _serviceRepository.DeleteAsync(service);
        }

        public async Task<ServiceIndexVM> GetAllAsync()
        {
            var services = await _serviceRepository.GetAllAsync();
            var model = new ServiceIndexVM
            {
                Services = services
            };
            return model;

        }

        public async Task<ServiceUpdateVM> GetUpdateModelAsync(int id)
        {
            var service = await _serviceRepository.GetAsync(id);
            var model = new ServiceUpdateVM
            {
                Id = service.Id,
                SubTitle = service.SubTitle,
                Title = service.Title,
                Description = service.Description
            };
            return model;
        }

        public async Task<bool> UpdateAsync(ServiceUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _serviceRepository.AnyAsync(s => s.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && s.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }
            var service = await _serviceRepository.GetAsync(model.Id);
            service.Title = model.Title;
            service.SubTitle = model.SubTitle;
            service.ModifiedAt = DateTime.Now;
            service.Description = model.Description;

            if (model.Photo != null)
            {
                var maxSize = 2000;
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
                _fileService.Delete(service.PhotoName);
                service.PhotoName = await _fileService.UploadAsync(model.Photo);
            }

            await _serviceRepository.UpdateAsync(service);
            return true;
        }
    }
}
