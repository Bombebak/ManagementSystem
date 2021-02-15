using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementSystem.Api.ViewComponents.Messages
{
    [ViewComponent(Name = "MessageList")]
    public class MessageListViewComponent : ViewComponent
    {
        private readonly ILogger<MessageListViewComponent> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageMapping _messageMapping;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageListViewComponent(ILogger<MessageListViewComponent> logger, IMessageRepository messageRepository, IMessageMapping messageMapping,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _messageRepository = messageRepository;
            _messageMapping = messageMapping;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(long taskId)
        {
            var user = await _userManager.GetUserAsync((ClaimsPrincipal)User);
            var messages = await _messageRepository.GetAllByTaskIdAsync(taskId);
            var result = _messageMapping.MapToListFromDatalayer(messages, user.Id);

            return View("_MessageList", result);
        }
    }
}
