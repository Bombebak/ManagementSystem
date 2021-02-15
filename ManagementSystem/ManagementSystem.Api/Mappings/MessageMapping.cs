using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.Enums;
using ManagementSystem.Api.Models.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Mappings
{
    public class MessageMapping : IMessageMapping
    {
        private readonly IFileService _fileService;
        private readonly IFileMapping _fileMapping;

        public MessageMapping(IFileService fileService, IFileMapping fileMapping)
        {
            _fileService = fileService;
            _fileMapping = fileMapping;
        }

        public List<MessageListViewModel> MapToListFromDatalayer(IEnumerable<ApplicationMessage> source, string currentUserId)
        {
            var target = new List<MessageListViewModel>();

            if (source != null)
            {
                foreach (var item in source)
                {
                    long? taskId = null;
                    target.Add(MapToListItemFromDatalayer(item, 0, currentUserId, ref taskId));
                }
            }

            return target;
        }

        private MessageListViewModel MapToListItemFromDatalayer(ApplicationMessage source, int level, string currentUserId, ref long? taskId)
        {
            var target = new MessageListViewModel();

            target.Author = new MessageAuthorViewModel()
            {
                Id = source.UserId,
                Name = source.User.UserName,
                ProfileImagePath = _fileService.GetUserProfileImage(source.User.ProfileImagePath)
            };
            target.Id = source.Id;
            if (source.TaskMessages != null && source.TaskMessages.Any())
            {
                taskId = source.TaskMessages.FirstOrDefault()?.TaskId;
            }
            var _taskId = taskId;

            target.TaskId = taskId.Value;
            target.CreationDateStr = source.CreationDate.ToString("f", CultureInfo.CreateSpecificCulture("da-dk"));
            target.Message = source.Message;
            target.Level = level;
            target.Files = _fileMapping.MapFileUploadedToList(source.MessageFiles);

            target.CurrentUserId = currentUserId;

            return target;
        }

        public MessageSaveRequestViewModel MapToViewModelFromDatalayer(ApplicationMessage source)
        {
            var target = new MessageSaveRequestViewModel();

            target.CreationDate = source.CreationDate;
            target.Id = source.Id;
            target.Message = source.Message;
            target.ExistingFiles = _fileMapping.MapFileUploadedToList(source.MessageFiles);

            return target;
        }
    }
}
