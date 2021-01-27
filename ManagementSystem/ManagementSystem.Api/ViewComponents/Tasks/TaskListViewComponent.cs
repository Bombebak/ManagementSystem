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
    [ViewComponent(Name = "TaskList")]
    public class TaskListViewComponent : ViewComponent
    {
        private readonly ILogger<TaskListViewComponent> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskMapping _taskMapping;

        public TaskListViewComponent(ILogger<TaskListViewComponent> logger, ITaskRepository taskRepository, ITaskMapping taskMapping)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _taskMapping = taskMapping;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = new List<TaskListViewModel>();

            try
            {
                var tasks = await _taskRepository.GetAllAsync();
                data = _taskMapping.MapToTaskList(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to load tasks for userId: ");
            }

            return View("_TaskList", data);
        }
    }
}
