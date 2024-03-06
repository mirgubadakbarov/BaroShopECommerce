using Web.Areas.Admin.ViewModels.Testimonial;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface ITestimonialService
    {
        Task<TestimonialIndexVM> GetAllAsync();
        Task<bool> CreateAsync(TestimonialCreateVM model);
        Task<TestimonialUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(TestimonialUpdateVM model);
        Task DeleteAsync(int id);
    }
}
