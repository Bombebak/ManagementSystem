using ManagementSystem.Api.Data;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Api;
using ManagementSystem.Api.Models.ViewModels.Sprint;
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
    public class SprintController : Controller, ISprintController
    {
        private readonly ILogger<SprintController> _logger;
        private readonly ISprintRepository _sprintRepository;
        private readonly ISprintMapping _sprintMapping;
        private readonly ApplicationDbContext _dbContext;
        private readonly IModelStateService _modelStateService;

        public SprintController(ILogger<SprintController> logger, ISprintRepository sprintRepository, ISprintMapping sprintMapping, 
            ApplicationDbContext dbContext, IModelStateService modelStateService)
        {
            _logger = logger;
            _sprintRepository = sprintRepository;
            _sprintMapping = sprintMapping;
            _dbContext = dbContext;
            _modelStateService = modelStateService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SaveSprint(long? sprintId)
        {
            return ViewComponent("SprintSave", sprintId);
        }


        [HttpPost]
        public async Task<IActionResult> SaveSprint(SaveSprintRequestViewModel request)
        {
            var result = new WebApiResult<SprintListViewModel>();

            result.Items = _modelStateService.ValidateRequest(ModelState);
            if (result.Items.Any())
            {
                return Json(new { result });
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                result.Success = await SaveItem(request, userId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Very nice",
                    ValidationType = Models.Enums.ValidationTypes.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to save sprintId: " + request.Id + " for userId: " + userId);
                result.Messages.Add(new ValidationItem()
                {
                    Message = "Not so good",
                    ValidationType = Models.Enums.ValidationTypes.Error
                });
            }

            return Json(new { result });
        }

        private async Task<bool> SaveItem(SaveSprintRequestViewModel request, string userId)
        {
            Data.Entities.ApplicationSprint sprintToBeSaved = new Data.Entities.ApplicationSprint();
            if (request.Id.GetValueOrDefault() != 0)
            {
                sprintToBeSaved = await _sprintRepository.GetByIdAsync(request.Id.Value);
            }
            else
            {
                _dbContext.Add(sprintToBeSaved);
            }
            sprintToBeSaved = _sprintMapping.MapDatalayerFromViewModel(sprintToBeSaved, request);
            var result = _dbContext.SaveChanges();
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}
