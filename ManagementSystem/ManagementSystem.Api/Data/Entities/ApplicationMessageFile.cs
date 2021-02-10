using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationMessageFile
    {
        public long Id { get; set; }
        public long MessageId { get; set; }
        public long? FileId { get; set; }

        public virtual ApplicationMessage Message { get; set; }
        public virtual ApplicationFile File { get; set; }
    }
}
