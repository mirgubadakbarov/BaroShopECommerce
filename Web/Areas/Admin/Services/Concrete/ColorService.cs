using Core.Entities;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Color;
using Web.Services.Abstract;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;
        private readonly ModelStateDictionary _modelState;

        public ColorService(IColorRepository colorRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _colorRepository = colorRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(ColorCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _colorRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This color already added");
                return false;
            }
            var color = new Color
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
            };
            await _colorRepository.CreateAsync(color);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var color = await _colorRepository.GetAsync(id);
            await _colorRepository.DeleteAsync(color);
        }

        public async Task<ColorIndexVM> GetAllAsync()
        {
            var model = new ColorIndexVM
            {
                Colors = await _colorRepository.GetAllAsync(),
            };
            return model;
        }

        public async Task<ColorUpdateVM> GetUpdateModelAsync(int id)
        {
            var color = await _colorRepository.GetAsync(id);
            var model = new ColorUpdateVM
            {
                Title = color.Title,
                Id = color.Id
            };
            return model;
        }

        public async Task<bool> UpdateAsync(ColorUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _colorRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && c.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This color already created");
                return false;
            }
            var color = await _colorRepository.GetAsync(model.Id);
            color.Title = model.Title;
            color.ModifiedAt = DateTime.Now;
            await _colorRepository.UpdateAsync(color);
            return true;
        }
    }
}
