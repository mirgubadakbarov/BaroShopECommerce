using Core.Entities;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Contact;

namespace Web.Areas.Admin.Services.Concrete
{
    public class ContactService : IContactService
    {
        private readonly ModelStateDictionary _modelState;
        private readonly IContactRepository _contactRepository;

        public ContactService(IActionContextAccessor actionContextAccessor,
            IContactRepository contactRepository)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _contactRepository = contactRepository;
        }
        public async Task<bool> CreateAsync(ContactCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var contact = new Contact
            {
                Address = model.Address,
                CreatedAt = DateTime.Now,
                Email = model.Email,
                MobileNumber = model.MobileNumber,
            };
            await _contactRepository.CreateAsync(contact);
            return true;

        }

        public async Task DeleteAsync()
        {
            var contact = await _contactRepository.GetAsync();
            await _contactRepository.DeleteAsync(contact);
        }

        public async Task<ContactIndexVM> GetAsync()
        {
            var contact = await _contactRepository.GetAsync();
            if (contact != null) { 
            var model = new ContactIndexVM
            {
                Address = contact.Address,
                Email = contact.Email,
                Id = contact.Id,
                MobileNumber = contact.MobileNumber,
                CreatedAt= contact.CreatedAt,
                ModifiedAt= contact.ModifiedAt,
            };
            return model;
            }
            return null;
        }

        public async Task<ContactUpdateVM> GetUpdateModelAsync()
        {
            var contact = await _contactRepository.GetAsync();
            var model = new ContactUpdateVM
            {
                Address = contact.Address,
                Email = contact.Email,
                Id = contact.Id,
                MobileNumber = contact.MobileNumber,
            };
            return model;
        }

        public async Task<bool> UpdateAsync(ContactUpdateVM model)
        {
            if (!_modelState.IsValid) return false;
            var contact = await _contactRepository.GetAsync();
            contact.Address = model.Address;
            contact.MobileNumber = model.MobileNumber;
            contact.Email = model.Email;
            contact.ModifiedAt = DateTime.Now;
            await _contactRepository.UpdateAsync(contact);
            return true;
        }
    }
}
