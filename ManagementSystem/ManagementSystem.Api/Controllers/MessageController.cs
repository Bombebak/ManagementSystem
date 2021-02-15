using ManagementSystem.Api.Data;
using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Api;
using ManagementSystem.Api.Models.ViewModels.Message;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Controllers
{
    public class MessageController : Controller, IMessageController
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageMapping _messageMapping;
        private readonly IModelStateService _modelStateService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileController _fileController;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(ILogger<MessageController> logger, IMessageRepository messageRepository, IMessageMapping messageMapping, IModelStateService 
            modelStateService, ApplicationDbContext dbContext, IFileController fileController)
        {
            _logger = logger;
            _messageRepository = messageRepository;
            _messageMapping = messageMapping;
            _modelStateService = modelStateService;
            _dbContext = dbContext;
            _fileController = fileController;
        }

        [HttpGet]
        public IActionResult SaveMessage(long? id, long? parentId, long taskId)
        {
            return ViewComponent("MessageSave", new { messageId = id, messageParentId = parentId, taskId = taskId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMessage(long id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            var data = new MessageDeleteViewModel()
            {
                Id = id
            };
            return PartialView("_ConfirmDeleteModal", data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage(MessageSaveRequestViewModel request)
        {
            var result = new WebApiResult<MessageListViewModel>();

            result.Items = _modelStateService.ValidateRequest(ModelState);
            if (result.Items.Any())
            {
                return Json(new { result });
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                var messageToBeSaved = await SaveMessage(request, userId);
                var files = _fileController.SaveFilesToDb(messageToBeSaved, request.Files, userId);
                List<Models.ViewModels.File.FileUploadedViewModel> existingFiles = _fileController.UpdateExistingFiles(messageToBeSaved.MessageFiles?.Where(e => e.Id != 0)?.ToList(), request.ExistingFiles);
                await _dbContext.SaveChangesAsync();
                result.Success = true;
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Very nice",
                    ValidationType = Models.Enums.ValidationTypes.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to save Message. UserID: " + userId + " messageId: " + request.Id);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Not so good",
                    ValidationType = Models.Enums.ValidationTypes.Error
                });
            }

            return Json(new { result });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMessage(MessageDeleteViewModel request)
        {
            var result = new WebApiResult<bool>();

            try
            {

                var itemToBeDeleted = await _messageRepository.GetByIdAsync(request.Id);
                var deleted = DeleteMessage(itemToBeDeleted);
                if (deleted)
                {
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to delete messageId: " + request.Id);
            }

            return Json(new { result });
        }

        private bool DeleteMessage(ApplicationMessage itemToBeDeleted)
        {
            _dbContext.Messages.Remove(itemToBeDeleted);
            _dbContext.TaskMessages.RemoveRange(itemToBeDeleted.TaskMessages);
            if (itemToBeDeleted.MessageFiles != null)
            {
                foreach (var msgFile in itemToBeDeleted.MessageFiles)
                {
                    DeleteFile(msgFile);
                }
            }
            return true;
        }

        private bool DeleteFile(ApplicationMessageFile msgFile)
        {
            _dbContext.MessageFiles.Remove(msgFile);
            _dbContext.Files.Remove(msgFile.File);
            return true;
        }

        private async Task<ApplicationMessage> SaveMessage(MessageSaveRequestViewModel request, string userId)
        {
            var messageToBeSaved = new ApplicationMessage();
            messageToBeSaved.CreationDate = DateTime.Now;
            if (request.Id.GetValueOrDefault() != 0)
            {
                messageToBeSaved = await _messageRepository.GetByIdAsync(request.Id.Value);
            }
            else
            {
                _dbContext.Messages.Add(messageToBeSaved);
                SaveMessageToTask(messageToBeSaved, request.TaskId.Value);
            }
            messageToBeSaved.Message = request.Message;
            messageToBeSaved.UserId = userId;

            return messageToBeSaved;
        }

        private void SaveMessageToTask(ApplicationMessage messageToBeSaved, long taskId)
        {
            
            var taskMessageToBeSaved = new ApplicationTaskMessage();
            taskMessageToBeSaved.TaskId = taskId;
            taskMessageToBeSaved.Message = messageToBeSaved;
            _dbContext.TaskMessages.Add(taskMessageToBeSaved);
        }
        
    }
}
