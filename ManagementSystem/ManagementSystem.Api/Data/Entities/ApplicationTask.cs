using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationTask
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public int? Hours { get; set; }
        public int? Minutes { get; set; }
        public int? RemainingHours { get; set; }
        public int? RemainingMinutes { get; set; }
        public int? TotalHours { get; set; }
        public int? TotalMinutes { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? SprintId { get; set; }
        public long? ProjectId { get; set; }

        public virtual ApplicationTask Parent { get; set; }
        public virtual ApplicationSprint Sprint { get; set; }
        public virtual ApplicationProject Project { get; set; }
        public virtual ICollection<ApplicationTaskUser> TaskUsers { get; set; }
        public virtual ICollection<ApplicationTimeRegistration> TimeRegistrations { get; set; }
        public virtual ICollection<ApplicationTask> Children { get; set; }
    }
}
