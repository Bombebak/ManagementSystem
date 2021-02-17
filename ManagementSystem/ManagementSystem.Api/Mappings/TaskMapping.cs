using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Task;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Mappings
{
    public class TaskMapping : ITaskMapping
    {
        private readonly ILogger<TaskMapping> _logger;
        private readonly IFileService _fileService;
        private readonly IFileMapping _fileMapping;

        public TaskMapping(ILogger<TaskMapping> logger, IFileService fileService, IFileMapping fileMapping)
        {
            _logger = logger;
            _fileService = fileService;
            _fileMapping = fileMapping;
        }

        public List<TaskListItemViewModel> MapToTaskList(IEnumerable<ApplicationTask> source)
        {
            var result = new List<TaskListItemViewModel>();

            if (source == null)
            {
                return result;
            }

            result.AddRange(source.Select(e => MapToTaskList(e, 0)));

            return result;
        }

        public SaveTaskRequestViewModel MapToSaveTask(ApplicationTask source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new SaveTaskRequestViewModel();

            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.ProjectId = source.ProjectId.GetValueOrDefault();
            target.SprintId = source.SprintId.GetValueOrDefault();
            target.ParentId = source.ParentId;
            target.ExistingFiles = _fileMapping.MapFileUploadedToList(source.TaskFiles);

            return target;
        }

        public void MapTaskToBeSaved(ApplicationTask target, SaveTaskRequestViewModel source)
        {
            target.Name = source.Name;
            target.Description = source.Description;
            target.Hours = source.Hours;
            target.Minutes = source.Minutes;
            if (source.ProjectId != 0)
            {
                target.ProjectId = source.ProjectId;
            }
            else
            {
                target.Project = null;
            }
            if (source.SprintId != 0)
            {
                target.SprintId = source.SprintId;
            }
            else
            {
                target.SprintId = null;
            }
            if (source.ParentId.HasValue)
            {
                target.ParentId = source.ParentId;
            }

        }

        public TaskListSelectedTaskViewModel MapToSelectedTask(ApplicationTask source)
        {
            var target = new TaskListSelectedTaskViewModel();

            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Hours = source.Hours;
            target.Minutes = source.Minutes;
            target.Sprint = source.Sprint?.Name;
            target.Project = source.Project?.Name;
            

            return target;
        }

        private TaskListItemViewModel MapToTaskList(ApplicationTask source, int level)
        {
            try
            {
                var target = new TaskListItemViewModel();

                target.Id = source.Id;
                target.Name = source.Name;
                target.ParentId = source.ParentId;
                target.Level = level;
                if (source.Children != null && source.Children.Any())
                {
                    level += 1;
                    target.Children.AddRange(source.Children.Select(e => MapToTaskList(e, level)));
                }
                if (source.TaskUsers != null && source.TaskUsers.Any())
                {
                    target.Users.AddRange(source.TaskUsers.Select(e => new TaskListUserItemViewModel(null, e.User.Email, _fileService.GetUserProfileImage(e.User.ProfileImagePath))));
                }

                return target;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured while trying to map datalayer object.", ex);
                _logger.LogInformation("source id : " + source.Id);
            }
            return null;
        }

    }
}
