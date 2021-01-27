using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationTaskUser
    {
        public long TaskId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationTask Task { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
