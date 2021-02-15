using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.ViewComponents.Messages
{
    [ViewComponent(Name = "MessageSave")]
    public class MessageSaveViewComponent : ViewComponent
    {
        private readonly ILogger<MessageSaveViewComponent> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageMapping _messageMapping;

        public MessageSaveViewComponent(ILogger<MessageSaveViewComponent> logger, IMessageRepository messageRepository, IMessageMapping messageMapping)
        {
            _logger = logger;
            _messageRepository = messageRepository;
            _messageMapping = messageMapping;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? messageId, long taskId)
        {
            var data = new MessageSaveRequestViewModel();
            if (messageId.HasValue)
            {
                try
                {
                    var entity = await _messageRepository.GetByIdAsync(messageId.Value);
                    if (entity == null)
                    {
                        _logger.LogInformation("Attempted to retrieve messageId: " + messageId);
                    }
                    data = _messageMapping.MapToViewModelFromDatalayer(entity);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception occured while trying to map messageId: " + messageId);
                }
            }
            data.TaskId = taskId;

            return View("_MessageSave", data);
        }
    }
}
