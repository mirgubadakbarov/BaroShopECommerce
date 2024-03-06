using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Fact;

namespace Web.Areas.Admin.Services.Concrete
{
    public class FactService : IFactService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IFileService _fileService;
        private readonly IFactRepository _factRepository;

        public FactService(IActionContextAccessor actionContextAccessor,
            IFileService fileService,
            IFactRepository factRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _fileService = fileService;
            _factRepository = factRepository;
        }
        public async Task<bool> CreateAsync(FactCreateVM model)
        {
            if (!_modelState.IsValid) return false;

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
            var fact = new Fact
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
            };
            await _factRepository.CreateAsync(fact);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var fact = await _factRepository.GetAsync(id);
            await _factRepository.DeleteAsync(fact);
        }

        public async Task<FactIndexVM> GetAllAsync()
        {
            var facts = await _factRepository.GetAllAsync();
            var model = new FactIndexVM
            {
                Facts = facts
            };
            return model;
        }

        public async Task<FactUpdateVM> GetUpdateModelAsync(int id)
        {
            var fact = await _factRepository.GetAsync(id);
            var model = new FactUpdateVM
            {
                Id = fact.Id,
                Description = fact.Description,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(FactUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var fact = await _factRepository.GetAsync(model.Id);
            fact.Description = model.Description;
            fact.ModifiedAt = DateTime.Now;

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

                _fileService.Delete(fact.PhotoName);
                fact.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _factRepository.UpdateAsync(fact);
            return true;

        }
    }
}
