using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.ViewComponents.Teams
{
    [ViewComponent(Name = "TeamList")]
    public class TeamListViewComponent : ViewComponent
    {
        private readonly ILogger<TeamListViewComponent> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamMapping _teamMapping;

        public TeamListViewComponent(ILogger<TeamListViewComponent> logger, ITeamRepository teamRepository, ITeamMapping teamMapping)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _teamMapping = teamMapping;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = new List<TeamListViewModel>();

            try
            {
                var tasks = await _teamRepository.GetAllAsync();
                var tasksWithoutChildren = tasks.Where(e => e.ParentId == null).ToList();
                data = _teamMapping.MapToTeamList(tasksWithoutChildren);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured while trying to load teams for userId: ");
            }

            return View("_TeamList", data);
        }
    }
}
