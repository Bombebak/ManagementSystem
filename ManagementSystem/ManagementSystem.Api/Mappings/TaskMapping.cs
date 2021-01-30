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

        public List<TaskListViewModel> MapToTaskList(IEnumerable<ApplicationTask> source)
        {
            var result = new List<TaskListViewModel>();

            if (source == null)
            {
                return result;
            }

            foreach (var item in source)
            {
                var target = MapToTaskList(item);
                if (target != null)
                {
                    result.Add(target);
                }
            }

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

            return target;
        }

        public ApplicationTask MapTaskToBeSaved(ApplicationTask target, SaveTaskRequestViewModel source)
        {
            if (target == null)
            {
                target = new ApplicationTask();
            }

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
                target.ProjectId = null;
            }
            if (source.SprintId != 0)
            {
                target.SprintId = source.SprintId;
            }
            else
            {
                target.SprintId = null;
            }

            return target;
        }

        private TaskListViewModel MapToTaskList(ApplicationTask source)
        {
            if (source == null)
            {
                return null;
            }
            try
            {
                var target = new TaskListViewModel();

                target.Id = source.Id;
                target.Name = source.Name;

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
