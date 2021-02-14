using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationTaskMessage
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public long MessageId { get; set; }

        public virtual ApplicationTask Task { get; set; }
        public virtual ApplicationMessage Message { get; set; }
    }
}
