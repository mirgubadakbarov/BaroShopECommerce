﻿using Core.Entities;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.QuestionCategory;

namespace Web.Areas.Admin.Services.Concrete
{
    public class QuestionCategoryService : IQuestionCategoryService
    {
        private readonly IQuestionCategoryRepository _questionCategoryRepository;
        private readonly ModelStateDictionary _modelState;

        public QuestionCategoryService(IQuestionCategoryRepository questionCategoryRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _questionCategoryRepository = questionCategoryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(QuestionCategoryCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _questionCategoryRepository.AnyAsync(ct => ct.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This category already created");
                return false;
            }
            var category = new QuestionCategory
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                Title = model.Title,
                ActiveStatus = model.ActiveStatus,
            };
            await _questionCategoryRepository.CreateAsync(category);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _questionCategoryRepository.GetAsync(id);
            await _questionCategoryRepository.DeleteAsync(category);
        }

        public async Task<QuestionCategoryIndexVM> GetAllAsync()
        {
            var categories = await _questionCategoryRepository.GetAllAsync();
            var model = new QuestionCategoryIndexVM
            {
                Categories = categories
            };
            return model;
        }

        public async Task<QuestionCategoryUpdateVM> GetUpdateModelAsync(int id)
        {
            var category = await _questionCategoryRepository.GetAsync(id);
            var model = new QuestionCategoryUpdateVM
            {
                Title = category.Title,
                Description = category.Description,
                ActiveStatus = category.ActiveStatus,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(QuestionCategoryUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _questionCategoryRepository.AnyAsync(ct => ct.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != ct.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This category already created");
                return false;
            }
            var category = await _questionCategoryRepository.GetAsync(model.Id);
            category.Title = model.Title;
            category.ModifiedAt = DateTime.Now;
            category.Description = model.Description;
            category.ActiveStatus = model.ActiveStatus;

            await _questionCategoryRepository.UpdateAsync(category);
            return true;
        }
    }
}
