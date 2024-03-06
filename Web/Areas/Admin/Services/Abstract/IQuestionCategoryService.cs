using Web.Areas.Admin.ViewModels.QuestionCategory;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IQuestionCategoryService
    {
        Task<bool> CreateAsync(QuestionCategoryCreateVM model);
        Task<QuestionCategoryIndexVM> GetAllAsync();
        Task<QuestionCategoryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(QuestionCategoryUpdateVM model);
        Task DeleteAsync(int id);
    }
}
