using ManagementSystem.Api.Data;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Api;
using ManagementSystem.Api.Models.ViewModels.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Controllers
{
    [Authorize]
    public class ProjectController : Controller, IProjectController
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMapping _projectMapping;
        private readonly ApplicationDbContext _dbContext;
        private readonly IModelStateService _modelStateService;

        public ProjectController(ILogger<ProjectController> logger, IProjectRepository projectRepository, IProjectMapping projectMapping, 
            ApplicationDbContext dbContext, IModelStateService modelStateService)
        {
            _logger = logger;
            _projectRepository = projectRepository;
            _projectMapping = projectMapping;
            _dbContext = dbContext;
            _modelStateService = modelStateService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SaveProject(long? projectId)
        {
            return ViewComponent("ProjectSave", projectId);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProject(SaveProjectRequestViewModel request)
        {
            var result = new WebApiResult<ProjectListViewModel>();

            result.Items = _modelStateService.ValidateRequest(ModelState);
            if (result.Items.Any())
            {
                return Json(new { result });
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                result.Success = await SaveItem(request);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Very nice",
                    ValidationType = Models.Enums.ValidationTypes.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to save projectId: " + request.Id + " for userId: " + userId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Not so good",
                    ValidationType = Models.Enums.ValidationTypes.Error
                });
            }

            return Json(new { result });
        }

        private async Task<bool> SaveItem(SaveProjectRequestViewModel request)
        {
            Data.Entities.ApplicationProject projectToBeSaved = new Data.Entities.ApplicationProject();
            if (request.Id.GetValueOrDefault() != 0)
            {
                projectToBeSaved = await _projectRepository.GetByIdAsync(request.Id.Value);
            }
            else
            {
                _dbContext.Add(projectToBeSaved);
            }

            projectToBeSaved = _projectMapping.MapDatalayerFromViewModel(projectToBeSaved, request);
            var result = _dbContext.SaveChanges();
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}
