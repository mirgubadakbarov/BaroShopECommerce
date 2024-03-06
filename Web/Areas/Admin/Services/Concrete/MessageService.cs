using Core.Entities;
using DataAccess.Repositories.Abstarct;
using MimeKit.Cryptography;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Message;

namespace Web.Areas.Admin.Services.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly ISendMessageRepository _messageRepository;

        public MessageService(ISendMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _messageRepository.GetAsync(id);
            await _messageRepository.DeleteAsync(message);
        }

        public async Task<MessageIndexVM> GetAllAsync()
        {
            var messages = await _messageRepository.GetAllAsync();
            var model = new MessageIndexVM { Messages = messages };
            return model;
        }

        public async Task<MessageDetailsVM> GetDetailsAsync(int id)
        {
            var message = await _messageRepository.GetAsync(id);
            var model = new MessageDetailsVM
            {
                Email = message.Email,
                FullName = message.FullName,
                Id = message.Id,
                Message = message.Message,
                Subject = message.Subject,
                RecieveTime = message.CreatedAt
            };

            return model;
        }

    }
}
