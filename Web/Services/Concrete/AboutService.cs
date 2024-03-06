using Core.Entities;
using DataAccess.Migrations;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels.About;

namespace Web.Services.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IBusinessInfoRepository _businessInfoRepository;
        private readonly IFactRepository _factRepository;
        private readonly IWhatWeDoRepository _whatWeDoRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISendMessageRepository _sendMessageRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ModelStateDictionary _modelState;

        public AboutService(IBusinessInfoRepository businessInfoRepository,
            IFactRepository factRepository,
            IWhatWeDoRepository whatWeDoRepository,
            IServiceRepository serviceRepository,
            ISendMessageRepository sendMessageRepository,
            IActionContextAccessor actionContextAccessor,
            IContactRepository contactRepository,
            ILocationRepository locationRepository)
        {
            _businessInfoRepository = businessInfoRepository;
            _factRepository = factRepository;
            _whatWeDoRepository = whatWeDoRepository;
            _serviceRepository = serviceRepository;
            _sendMessageRepository = sendMessageRepository;
            _contactRepository = contactRepository;
            _locationRepository = locationRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<AboutIndexVM> GetAllAsync()
        {
            var model = new AboutIndexVM
            {
                BusinessInfos = await _businessInfoRepository.GetAllAsync(),
                Facts = await _factRepository.GetAllAsync(),
                WhatWedo = await _whatWeDoRepository.GetAsync(),
                Services = await _serviceRepository.GetAllAsync(),
                ContactInfo = await _contactRepository.GetAsync(),
                Map = await _locationRepository.GetAsync(),
            };
            return model;
        }

        public async Task<bool> SendMessageAsync(SendMessage message)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _sendMessageRepository.AnyAsync(msg =>
            msg.FullName.Trim().ToLower() == message.FullName.Trim().ToLower() &&
            msg.Email.Trim().ToLower() == message.Email.Trim().ToLower() &&
            msg.Subject.Trim().ToLower() == message.Subject.Trim().ToLower());

            if (isExist)
            {
                _modelState.AddModelError("Subject", "This message already sent");
                return false;
            }

            var newMessage = new SendMessage
            {
                CreatedAt = DateTime.Now,
                Email = message.Email,
                FullName = message.FullName,
                Message = message.Message,
                Subject = message.Subject,
                IsSend = true
            };

            var model = new AboutIndexVM { IsSent = true };
            await _sendMessageRepository.CreateAsync(newMessage);
            return true;
        }
    }
}
