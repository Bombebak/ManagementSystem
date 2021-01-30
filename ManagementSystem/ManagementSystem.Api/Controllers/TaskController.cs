using ManagementSystem.Api.Data;
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
        private readonly ICommonListItemController _commonListItemController;
        private readonly IModelStateService _modelStateService;

        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository, ITaskMapping taskMapping, ApplicationDbContext dbContext, 
            ICommonListItemController commonListItemController, IModelStateService modelStateService)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _taskMapping = taskMapping;
            _dbContext = dbContext;
            _commonListItemController = commonListItemController;
            _modelStateService = modelStateService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadTasks()
        {
            var data = new List<TaskListViewModel>();
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                var tasks = await _taskRepository.GetByUserIdAsync(userId);
                data = _taskMapping.MapToTaskList(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to load tasks for userId: " + userId);
            }

            return ViewComponent("_TaskList");
        }

        [HttpGet]
        public IActionResult SaveTask(long? taskId)
        {
            return ViewComponent("TaskSave", taskId);
        }

        
        [HttpPost]
        public IActionResult SaveTask(SaveTaskRequestViewModel request)
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
                result.Success = SaveTask(request, userId);
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

        private bool SaveTask(SaveTaskRequestViewModel request, string userId)
        {
            Data.Entities.ApplicationTask taskToBeSaved = new Data.Entities.ApplicationTask();
            if (request.Id.GetValueOrDefault() != 0)
            {
                taskToBeSaved = _taskRepository.GetById(request.Id.Value);
            }
            else
            {
                var taskUserEntity = new Data.Entities.ApplicationTaskUser();
                taskUserEntity.UserId = userId;
                taskUserEntity.Task = taskToBeSaved;
                _dbContext.Add(taskUserEntity);
                _dbContext.Add(taskToBeSaved);
            }
            taskToBeSaved = _taskMapping.MapTaskToBeSaved(taskToBeSaved, request);
            var result = _dbContext.SaveChanges();
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}
