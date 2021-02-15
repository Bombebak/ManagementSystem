using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationMessage
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<ApplicationMessageFile> MessageFiles { get; set; }
        public virtual ICollection<ApplicationTaskMessage> TaskMessages { get; set; }
    }
}
