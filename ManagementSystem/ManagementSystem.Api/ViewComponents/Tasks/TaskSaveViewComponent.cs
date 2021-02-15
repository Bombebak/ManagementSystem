using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.ViewComponents.Tasks
{
    [ViewComponent(Name = "TaskSave")]
    public class TaskSaveViewComponent : ViewComponent
    {
        private readonly ILogger<TaskListViewComponent> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskMapping _taskMapping;
        private readonly ICommonListItemController _commonListItemController;

        public TaskSaveViewComponent(ILogger<TaskListViewComponent> logger, ITaskRepository taskRepository, ITaskMapping taskMapping, ICommonListItemController commonListItemController)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _taskMapping = taskMapping;
            _commonListItemController = commonListItemController;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? taskId, long? taskParentId)
        {
            SaveTaskRequestViewModel data = new SaveTaskRequestViewModel();
            if (taskId.HasValue)
            {
                var entity = await _taskRepository.GetByIdAsync(taskId.Value);
                data = _taskMapping.MapToSaveTask(entity);
            }
            if (taskParentId.HasValue)
            {
                data.ParentId = taskParentId;
            }
            data.Projects = await _commonListItemController.GetProjectsAsync(true);
            data.Sprints = await _commonListItemController.GetSprintsAsync(true);

            return View("_TaskSave", data);
        }
    }
}
