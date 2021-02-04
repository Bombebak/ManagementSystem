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
        private readonly IUserRepository _userRepository;

        public TaskListViewComponent(ILogger<TaskListViewComponent> logger, ITaskRepository taskRepository, ITaskMapping taskMapping, IUserRepository userRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _taskMapping = taskMapping;
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(TaskListCriteriaViewModel criterias)
        {
            var result = new List<TaskListItemViewModel>();

            if (criterias == null)
            {
                criterias = new TaskListCriteriaViewModel();
            }
            
            result = await GetAvailableTasks(criterias);

            return View("_TaskList", result);
        }

        private async Task<List<TaskListItemViewModel>> GetAvailableTasks(TaskListCriteriaViewModel criterias)
        {
            var tasks = new List<TaskListItemViewModel>();
            try
            {
                var userIds = new List<string>();
                if (criterias.UserEmails != null && criterias.UserEmails.Any())
                {
                    foreach (var item in criterias.UserEmails)
                    {
                        var userId = _userRepository.GetUserByEmailAsync(item);
                        userIds.Add(userId.Result.Id);
                    }
                }
                var items = await _taskRepository.GetAllWithoutToList(criterias.SearchText, criterias.ProjectId, criterias.SprintId, userIds);                
                var itemsWithoutChildren = items.Where(e => e.ParentId == null).ToList();
                tasks = _taskMapping.MapToTaskList(itemsWithoutChildren);
                tasks.AddRange(_taskMapping.MapToTaskList(itemsWithoutChildren));
                tasks.AddRange(_taskMapping.MapToTaskList(itemsWithoutChildren));
                tasks.AddRange(_taskMapping.MapToTaskList(itemsWithoutChildren));
                tasks.AddRange(_taskMapping.MapToTaskList(itemsWithoutChildren));
                tasks.AddRange(_taskMapping.MapToTaskList(itemsWithoutChildren));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to load tasks for userId: ");
                tasks = new List<TaskListItemViewModel>();
            }
            return tasks;
        }
        
    }
}
