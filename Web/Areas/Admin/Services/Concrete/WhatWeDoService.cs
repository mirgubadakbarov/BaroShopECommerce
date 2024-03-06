using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.WhatWeDo;

namespace Web.Areas.Admin.Services.Concrete
{
    public class WhatWeDoService : IWhatWeDoService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IFileService _fileService;
        private readonly IWhatWeDoRepository _whatWeDoRepository;

        public WhatWeDoService(IActionContextAccessor actionContextAccessor,
            IFileService fileService,
            IWhatWeDoRepository whatWeDoRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _fileService = fileService;
            _whatWeDoRepository = whatWeDoRepository;
        }
        public async Task<bool> CreateAsync(WhatWeDoCreateVM model)
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
            var whatWeDo = new WhatWedo
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.Photo),
                Title = model.Title,
            };
            await _whatWeDoRepository.CreateAsync(whatWeDo);
            return true;
        }

        public async Task DeleteAsync()
        {
            var whatWeDo = await _whatWeDoRepository.GetAsync();
            await _whatWeDoRepository.DeleteAsync(whatWeDo);
        }

        public async Task<WhatWeDoIndexVM> GetAsync()
        {
            var whatWeDo = await _whatWeDoRepository.GetAsync();
            var model = new WhatWeDoIndexVM
            {
                WhatWedo = whatWeDo
            };
            return model;
        }

        public async Task<WhatWeDoUpdateVM> GetUpdateModelAsync()
        {
            var whatWeDo = await _whatWeDoRepository.GetAsync();
            var model = new WhatWeDoUpdateVM
            {
                Description = whatWeDo.Description,
                Id = whatWeDo.Id,
                Title = whatWeDo.Title,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(WhatWeDoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var whatWeDo = await _whatWeDoRepository.GetAsync();

            whatWeDo.Description = model.Description;
            whatWeDo.ModifiedAt = DateTime.Now;
            whatWeDo.Title = model.Title;
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
                _fileService.Delete(whatWeDo.PhotoName);
                whatWeDo.PhotoName = await _fileService.UploadAsync(model.Photo);
            }
            await _whatWeDoRepository.UpdateAsync(whatWeDo);
            return true;
        }
    }
}
