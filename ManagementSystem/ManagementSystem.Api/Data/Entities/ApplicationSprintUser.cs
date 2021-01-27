using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationSprintUser
    {
        public long SprintId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationSprint Sprint { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
