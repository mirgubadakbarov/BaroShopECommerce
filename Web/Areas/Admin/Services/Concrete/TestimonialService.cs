using Core.Entities;
using Core.Utilities.FileService;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Testimonial;

namespace Web.Areas.Admin.Services.Concrete
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly ITestimonialRepository _testimonialRepository;
        private readonly IFileService _fileService;

        public TestimonialService(IActionContextAccessor actionContextAccessor,
            ITestimonialRepository testimonialRepository,
            IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _testimonialRepository = testimonialRepository;
            _fileService = fileService;
        }
        public async Task<bool> CreateAsync(TestimonialCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _testimonialRepository.AnyAsync(t => t.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }
            var maxSize = 3000;
            if (!_fileService.CheckPhoto(model.UserPhoto))
            {
                _modelState.AddModelError("UserPhoto", "File must be image format");
                return false;
            }
            else if (!_fileService.MaxSize(model.UserPhoto, maxSize))
            {
                _modelState.AddModelError("UserPhoto", $"Photo size must be less than {maxSize} kb;");
                return false;
            }

            var testimonial = new Testimonial
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                PhotoName = await _fileService.UploadAsync(model.UserPhoto),
                Title = model.Title,
            };
            await _testimonialRepository.CreateAsync(testimonial);
            return true;

        }

        public async Task DeleteAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetAsync(id);
            await _testimonialRepository.DeleteAsync(testimonial);
        }

        public async Task<TestimonialIndexVM> GetAllAsync()
        {
            var testimonials = await _testimonialRepository.GetAllAsync();
            var model = new TestimonialIndexVM
            {
                Testimonials = testimonials
            };
            return model;
        }

        public async Task<TestimonialUpdateVM> GetUpdateModelAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetAsync(id);
            var model = new TestimonialUpdateVM
            {
                Id = testimonial.Id,
                Description = testimonial.Description,
                Title = testimonial.Title,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(TestimonialUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _testimonialRepository.AnyAsync(t => t.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && t.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }
            var testimonial = await _testimonialRepository.GetAsync(model.Id);
            testimonial.Title = model.Title;
            testimonial.ModifiedAt = DateTime.Now;
            testimonial.Description = model.Description;
            if (model.UserPhoto != null)
            {
                var maxSize = 3000;
                if (!_fileService.CheckPhoto(model.UserPhoto))
                {
                    _modelState.AddModelError("UserPhoto", "File must be image format");
                    return false;
                }
                else if (!_fileService.MaxSize(model.UserPhoto, maxSize))
                {
                    _modelState.AddModelError("UserPhoto", $"Photo size must be less than {maxSize} kb;");
                    return false;
                }
                _fileService.Delete(testimonial.PhotoName);
                testimonial.PhotoName = await _fileService.UploadAsync(model.UserPhoto);
            }
            await _testimonialRepository.UpdateAsync(testimonial);
            return true;
        }
    }
}
