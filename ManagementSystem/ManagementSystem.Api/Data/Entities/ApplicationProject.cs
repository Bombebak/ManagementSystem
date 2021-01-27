using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationProject
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ApplicationTask> Tasks { get; set; }
    }
}
