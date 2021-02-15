using ManagementSystem.Api.Data;
using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Api;
using ManagementSystem.Api.Models.ViewModels.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Controllers
{
    [Authorize]
    public class TaskController : Controller, ITaskController
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskMapping _taskMapping;
        private readonly ApplicationDbContext _dbContext;
        private readonly IModelStateService _modelStateService;
        private readonly ICommonListItemController _commonListItemController;
        private readonly IUserRepository _userRepository;


        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository, ITaskMapping taskMapping, ApplicationDbContext dbContext, 
            IModelStateService modelStateService, ICommonListItemController commonListItemController, IUserRepository userRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _taskMapping = taskMapping;
            _dbContext = dbContext;
            _modelStateService = modelStateService;
            _commonListItemController = commonListItemController;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var criterias = await InitializeCriterias(null);
            return View("Index", criterias);
        }

        [HttpGet]
        public IActionResult SaveTask(long? taskId, long? taskParentId)
        {
            return ViewComponent("TaskSave", new { taskId, taskParentId });
        }

        [HttpGet]
        public async Task<IActionResult> LoadSelectedTask(long? taskId)
        {
            if (taskId.GetValueOrDefault() == 0)
            {
                //TODO: Error handling
            }
            var selectedTask = await _taskRepository.GetByIdAsync(taskId.Value);
            if (selectedTask == null)
            {
                //TODO: Error handling
            }

            var vm = _taskMapping.MapToSelectedTask(selectedTask);

            return PartialView("_TaskListSelectedTask", vm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTask(SaveTaskRequestViewModel request)
        {
            var result = new WebApiResult<TaskListViewModel>();

            result.Items = _modelStateService.ValidateRequest(ModelState);
            if (result.Items.Any())
            {
                return Json(new { result });
            }            
            
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {                
                result.Success = await SaveTask(request, userId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Very nice",
                    ValidationType = Models.Enums.ValidationTypes.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to save taskId: " + request.Id + " for userId: " + userId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Not so good",
                    ValidationType = Models.Enums.ValidationTypes.Error
                });
            }

            return Json(new { result });
        }

        private async Task<bool> SaveTask(SaveTaskRequestViewModel request, string userId)
        {
            var taskToBeSaved = new ApplicationTask();
            if (request.Id.GetValueOrDefault() != 0)
            {
                taskToBeSaved = await _taskRepository.GetByIdAsync(request.Id.Value);
            }
            else
            {
                _dbContext.Add(taskToBeSaved);
            }
            await SaveTaskUsers(taskToBeSaved, request.TaskUsers);
            _taskMapping.MapTaskToBeSaved(taskToBeSaved, request);
            var result = _dbContext.SaveChanges();

            if (result == 1)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> SaveTaskUsers(ApplicationTask taskToBeSaved, List<string> TaskUsersToBeSaved)
        {
            if (TaskUsersToBeSaved == null)
            {
                TaskUsersToBeSaved = new List<string>();
            }
            DeleteExistingTaskUsers(taskToBeSaved, TaskUsersToBeSaved);
            await AddNewTaskUsers(taskToBeSaved, TaskUsersToBeSaved);
            return true;
        }

        private void DeleteExistingTaskUsers(ApplicationTask taskToBeSaved, List<string> TaskUsersToBeSaved)
        {
            var taskUsersToBeDeleted = new List<ApplicationTaskUser>();
            var existingTaskUsers = taskToBeSaved.TaskUsers;
            if (existingTaskUsers != null)
            {
                foreach (var taskUser in existingTaskUsers)
                {
                    if (TaskUsersToBeSaved.FirstOrDefault(e => e == taskUser.User.Email) == null)
                    {
                        taskUsersToBeDeleted.Add(taskUser);
                    }
                }
            }
            _dbContext.TaskUsers.RemoveRange(taskUsersToBeDeleted);
        }

        private async Task<bool> AddNewTaskUsers(ApplicationTask taskToBeSaved, List<string> TaskUsersToBeSaved)
        {
            var taskUsersToAdd = new List<ApplicationTaskUser>();
            var existingTaskUsers = taskToBeSaved.TaskUsers?.Select(e => e.User);
            foreach (var userEmail in TaskUsersToBeSaved)
            {
                if (existingTaskUsers?.FirstOrDefault(e => e.Email == userEmail) == null)
                {
                    var taskUserToAdd = new ApplicationTaskUser()
                    {
                        User = await _userRepository.GetUserByEmailAsync(userEmail),
                        Task = taskToBeSaved
                    };
                    taskUsersToAdd.Add(taskUserToAdd);
                }
            }
            _dbContext.TaskUsers.AddRange(taskUsersToAdd);
            return true;
        }

        [HttpPost]
        public IActionResult SearchTasks(TaskListCriteriaViewModel criterias)
        {
            return ViewComponent("TaskList", new { criterias });
        }

        private async Task<TaskListCriteriaViewModel> InitializeCriterias(TaskListCriteriaViewModel criterias)
        {
            if (criterias == null)
            {
                criterias = new TaskListCriteriaViewModel();
            }
            if (criterias.Projects == null || !criterias.Projects.Any())
            {
                criterias.Projects = await _commonListItemController.GetProjectsAsync(true);
            }
            if (criterias.Sprints == null || !criterias.Sprints.Any())
            {
                criterias.Sprints = await _commonListItemController.GetSprintsAsync(true);
            }
            if (criterias.UserEmails == null || !criterias.UserEmails.Any())
            {

            }
            return criterias;
        }
    }
}
