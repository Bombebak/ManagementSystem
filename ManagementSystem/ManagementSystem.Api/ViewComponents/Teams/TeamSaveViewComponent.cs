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
    [ViewComponent(Name = "TeamSave")]
    public class TeamSaveViewComponent : ViewComponent
    {
        private readonly ILogger<TeamSaveViewComponent> _logger;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamMapping _teamMapping;

        public TeamSaveViewComponent(ILogger<TeamSaveViewComponent> logger, ITeamRepository teamRepository, ITeamMapping teamMapping)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _teamMapping = teamMapping;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? teamId, long? teamParentId)
        {
            var data = new TeamSaveRequestViewModel();
            if (teamId.HasValue)
            {
                var entity = await _teamRepository.GetByIdAsync(teamId.Value);
                data = _teamMapping.MapViewModelFromDatalayer(entity);
            }
            if (teamParentId.HasValue)
            {
                data.ParentId = teamParentId;
            }
            
            return View("_TeamSave", data);
        }
    }
}
