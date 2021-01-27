using ManagementSystem.Api.Data;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Api;
using ManagementSystem.Api.Models.ViewModels.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Controllers
{
    public class TaskController : Controller, ITaskController
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskMapping _taskMapping;
        private readonly ApplicationDbContext _dbContext;

        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository, ITaskMapping taskMapping, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _taskMapping = taskMapping;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = _dbContext.TaskUsers.ToList();
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

            return ViewComponent("_TaskList", data);
        }

        [HttpGet]
        public IActionResult SaveTask(long? taskId)
        {
            SaveTaskRequestViewModel data = new SaveTaskRequestViewModel();
            if (taskId.HasValue)
            {
                var entity = _taskRepository.GetById(taskId.Value);
                data = _taskMapping.MapToSaveTask(entity);
            }

            return View("_SaveTask", data);
        }

        [HttpPost]
        public IActionResult SaveTask(SaveTaskRequestViewModel request)
        {
            if (!ModelState.IsValid) {
                return PartialView("_SaveTask", request);
            }
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var taskEntity = new Data.Entities.ApplicationTask();
            taskEntity.Name = request.Name;
            taskEntity.Description = request.Description;

            var taskUserEntity = new Data.Entities.ApplicationTaskUser();
            taskUserEntity.UserId = userId;
            taskUserEntity.Task = taskEntity;

            var result = new WebApiResult<List<TaskListViewModel>>();

            try
            {
                _dbContext.Add(taskEntity);
                _dbContext.Add(taskUserEntity);
                _dbContext.SaveChanges();
                result.Success = true;
            }
            catch (Exception ex)
            {

            }

            return ViewComponent("TaskList");
        }
    }
}
