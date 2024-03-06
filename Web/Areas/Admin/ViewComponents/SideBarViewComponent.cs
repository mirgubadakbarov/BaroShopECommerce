using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.ViewModels.Components;

namespace Web.Areas.Admin.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ISendMessageRepository _messageRepository;

        public SideBarViewComponent(ISendMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SideBarComponentVM
            {
                MessageCount=await _messageRepository.GetMessageCountAsync(),   
            };
            return View(model);
        }

    }

}
