using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Models.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface ITeamMapping
    {
        List<TeamListViewModel> MapToTeamList(List<ApplicationTeam> source);
        ApplicationTeam MapDatalayerFromViewModel(ApplicationTeam target, TeamSaveRequestViewModel source);
        TeamSaveRequestViewModel MapViewModelFromDatalayer(ApplicationTeam source);
    }
}
