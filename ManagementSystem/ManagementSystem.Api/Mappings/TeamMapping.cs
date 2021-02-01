using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.ViewModels.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Mappings
{
    public class TeamMapping : ITeamMapping
    {
        public ApplicationTeam MapDatalayerFromViewModel(ApplicationTeam target, TeamSaveRequestViewModel source)
        {
            if (target == null)
            {
                target = new ApplicationTeam();
            }

            target.Name = source.Name;
            target.ParentId = source.ParentId;

            return target;
        }

        public TeamSaveRequestViewModel MapViewModelFromDatalayer(ApplicationTeam source)
        {
            return new TeamSaveRequestViewModel
            {
                Id = source.Id,
                Name = source.Name
            };
        }

        public List<TeamListViewModel> MapToTeamList(List<ApplicationTeam> source)
        {
            var target = new List<TeamListViewModel>();

            target.AddRange(source.Select(e => MapToTeamList(e)));

            return target;
        }

        private TeamListViewModel MapToTeamList(ApplicationTeam source)
        {
            var target = new TeamListViewModel
            {
                Id = source.Id,
                Name = source.Name,
                ParentId = source.ParentId,
                Children = new List<TeamListViewModel>(),
                Users = new List<Models.ViewModels.TeamUser.TeamUserViewModel>()
            };
            if (source.Children != null && source.Children.Any())
            {
                target.Children.AddRange(source.Children.Select(e => MapToTeamList(e)));
            }
            if (source.TeamUsers != null && source.TeamUsers.Any())
            {
                //target.Users.AddRange(source.TeamUsers.Select(e => MapToTeamList(e)));
            }

            return target;
        }
    }
}
