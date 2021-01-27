using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationTeamUser
    {
        public string UserId { get; set; }
        public long TeamId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationTeam Team { get; set; }
    }
}
