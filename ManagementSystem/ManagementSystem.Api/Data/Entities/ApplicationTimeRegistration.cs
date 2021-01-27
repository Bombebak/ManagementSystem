using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationTimeRegistration
    {
        public long Id { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public bool isApproved { get; set; }
        public string UserId { get; set; }
        public long TaskId { get; set; }

        public virtual ApplicationTask Task { get; set; }
        public virtual ApplicationUser User { get; set; }        
    }
}
