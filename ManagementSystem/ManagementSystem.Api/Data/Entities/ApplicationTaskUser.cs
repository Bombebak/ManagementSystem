using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationTaskUser
    {
        [Key]
        public long Id { get; set; }
        public long TaskId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationTask Task { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
