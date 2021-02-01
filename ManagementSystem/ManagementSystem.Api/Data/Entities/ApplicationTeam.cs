using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationTeam
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Name { get; set; }
        public long TeamType { get; set; }

        public virtual ApplicationTeam Parent { get; set; }
        public virtual ICollection<ApplicationTeamUser> TeamUsers { get; set; }
        public virtual ICollection<ApplicationTeam> Children { get; set; }
    }
}
