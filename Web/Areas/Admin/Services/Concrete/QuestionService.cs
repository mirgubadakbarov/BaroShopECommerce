using Core.Entities;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Services.Concrete
{
    public class QuestionService : IQuestionService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionCategoryRepository _questionCategoryRepository;

        public QuestionService(IQuestionRepository questionRepository,
            IActionContextAccessor actionContextAccessor,
            IQuestionCategoryRepository questionCategoryRepository)
        {

            _modelState = actionContextAccessor.ActionContext.ModelState;
            _questionRepository = questionRepository;
            _questionCategoryRepository = questionCategoryRepository;
        }
        public async Task<bool> CreateAsync(QuestionCreateVM model)
        {
            model.Categories = await GetCategoriesAsync();

            if (!_modelState.IsValid) return false;
            var isExist = await _questionRepository.AnyAsync(q => q.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This question already created");
                return false;
            }
            var question = new Question
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                QuestionCategoryId = model.CategoryId,
                Title = model.Title,
            };
            await _questionRepository.CreateAsync(question);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var question = await _questionRepository.GetAsync(id);
            await _questionRepository.DeleteAsync(question);
        }

        public async Task<List<SelectListItem>> GetCategoriesAsync()
        {
            var categories = await _questionCategoryRepository.GetAllAsync();

            var selectCategory = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title

            }).ToList();
            return selectCategory;
        }

        public async Task<QuestionCreateVM> GetCategoriesCreateModelAsync()
        {
            var categories = await _questionCategoryRepository.GetAllAsync();
            var model = new QuestionCreateVM
            {
                Categories = categories.Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList()
            };
            return model;
        }

        public async Task<QuestionIndexVM> GetQuestionWithCategoryAsync()
        {
            var model = new QuestionIndexVM
            {
                Questions = await _questionRepository.GetQuestionsWithCategory()
            };
            return model;
        }

        public async Task<QuestionUpdateVM> GetUpdateModelAsync(int id)
        {
            var categories = await GetCategoriesAsync();
            var question = await _questionRepository.GetAsync(id);
            var model = new QuestionUpdateVM
            {
                Categories = categories,
                Description = question.Description,
                Title = question.Title,
                Id = question.Id
            };
            return model;
        }

        public async Task<bool> UpdateAsync(QuestionUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _questionRepository.AnyAsync(ct => ct.Title.Trim().ToLower() == model.Title.Trim().ToLower() &&
            ct.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This question already created");
                return false;
            }
            var question = await _questionRepository.GetAsync(model.Id);
            question.Description = model.Description;
            question.QuestionCategoryId = model.CategoryId;
            question.Title = model.Title;
            question.ModifiedAt = DateTime.Now;

            await _questionRepository.UpdateAsync(question);
            return true;
        }
    }
}
