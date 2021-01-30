using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Sprint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.ViewComponents.Sprints
{
    [ViewComponent(Name = "SprintSave")]
    public class SprintSaveViewComponent : ViewComponent
    {
        private readonly ILogger<SprintSaveViewComponent> _logger;
        private readonly ISprintRepository _sprintRepository;
        private readonly ISprintMapping _sprintMapping;

        public SprintSaveViewComponent(ILogger<SprintSaveViewComponent> logger, ISprintRepository sprintRepository, ISprintMapping sprintMapping)
        {
            _logger = logger;
            _sprintRepository = sprintRepository;
            _sprintMapping = sprintMapping;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? taskId)
        {
            var data = new SaveSprintRequestViewModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            if (taskId.HasValue)
            {
                var entity = await _sprintRepository.GetByIdAsync(taskId.Value);
                data = _sprintMapping.MapViewModelFromDatalayer(entity);
            }

            return View("_SprintSave", data);
        }
    }
}
