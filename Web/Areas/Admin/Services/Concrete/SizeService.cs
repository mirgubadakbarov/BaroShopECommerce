using Core.Entities;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Color;
using Web.Areas.Admin.ViewModels.Size;

namespace Web.Areas.Admin.Services.Concrete
{
    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly ModelStateDictionary _modelState;

        public SizeService(ISizeRepository sizeRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _sizeRepository = sizeRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(SizeCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _sizeRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This size already added");
                return false;
            }
            var size = new Size
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };
            await _sizeRepository.CreateAsync(size);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var size = await _sizeRepository.GetAsync(id);
            await _sizeRepository.DeleteAsync(size);
        }

        public async Task<SizeIndexVM> GetAllAsync()
        {
            var model = new SizeIndexVM
            {
                Sizes = await _sizeRepository.GetAllAsync(),
            };
            return model;
        }

        public async Task<SizeUpdateVM> GetUpdateModelAsync(int id)
        {
            var size = await _sizeRepository.GetAsync(id);
            var model = new SizeUpdateVM
            {
                Title = size.Title,
                Id = size.Id
            };
            return model;
        }

        public async Task<bool> UpdateAsync(SizeUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _sizeRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This size already created");
                return false;
            }
            var color = await _sizeRepository.GetAsync(model.Id);
            color.Title = model.Title;
            color.ModifiedAt = DateTime.Now;
            await _sizeRepository.UpdateAsync(color);
            return true;
        }
    }
}
