using Core.Entities;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.OurService;

namespace Web.Areas.Admin.Services.Concrete
{
    public class OurServiceService : IOurServiceService
    {
        private readonly IOurServiceRepository _ourServiceRepository;
        private readonly ModelStateDictionary _modelState;

        public OurServiceService(IOurServiceRepository ourServiceRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _ourServiceRepository = ourServiceRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<bool> CreateAsync(OurServiceCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _ourServiceRepository.AnyAsync(os => os.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }
            var ourService = new OurService
            {
                CreatedAt = DateTime.Now,
                Description = model.Description,
                Icon = model.Icon,
                Title = model.Title,
            };
            await _ourServiceRepository.CreateAsync(ourService);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var ourService = await _ourServiceRepository.GetAsync(id);
            await _ourServiceRepository.DeleteAsync(ourService);
        }

        public async Task<OurServiceIndexVM> GetAllAsync()
        {
            var ourService = await _ourServiceRepository.GetAllAsync();
            var model = new OurServiceIndexVM
            {
                OurServices = ourService,
            };
            return model;
        }

        public async Task<OurServiceUpdateVM> GetUpdateModelAsync(int id)
        {
            var ourService = await _ourServiceRepository.GetAsync(id);
            var model = new OurServiceUpdateVM
            {
                Description = ourService.Description,
                Icon = ourService.Icon,
                Title = ourService.Title,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(OurServiceUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _ourServiceRepository.AnyAsync(os => os.Title.Trim().ToLower() == model.Title.Trim().ToLower()
            && model.Id != os.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This content already created");
                return false;
            }
            var ourservice = await _ourServiceRepository.GetAsync(model.Id);
            ourservice.Title = model.Title;
            ourservice.Description = model.Description;
            ourservice.ModifiedAt = DateTime.Now;
            ourservice.Icon = model.Icon;

            await _ourServiceRepository.UpdateAsync(ourservice);
            return true;
        }
    }
}
