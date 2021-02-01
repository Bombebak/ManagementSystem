using ManagementSystem.Api.Models.ViewModels.TeamUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Models.ViewModels.Team
{
    public class TeamSaveRequestViewModel
    {
        public long? Id { get; set; }
        public long? ParentId { get; set; }
        public string Name { get; set; }
        public List<TeamSaveRequestViewModel> Children { get; set; }
        public List<TeamUserViewModel> Users { get; set; }
        public List<string> UserEmailsInTeam { get; set; }
    }
}
