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
    [ViewComponent(Name = "SprintList")]
    public class SprintListViewComponent : ViewComponent
    {
        private readonly ILogger<SprintListViewComponent> _logger;
        private readonly ISprintRepository _sprintRepository;
        private readonly ISprintMapping _sprintMapping;

        public SprintListViewComponent(ILogger<SprintListViewComponent> logger, ISprintRepository sprintRepository, ISprintMapping sprintMapping)
        {
            _logger = logger;
            _sprintRepository = sprintRepository;
            _sprintMapping = sprintMapping;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = new List<SprintListViewModel>();

            try
            {
                var tasks = await _sprintRepository.GetAllAsync();
                data = _sprintMapping.MapToSprintList(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to load tasks for userId: ");
            }

            return View("_SprintList", data);
        }
    }
}
