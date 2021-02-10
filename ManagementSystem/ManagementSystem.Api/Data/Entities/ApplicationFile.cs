using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationFile
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength]
        public byte[] Content { get; set; }
        public int FileType { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ApplicationUser User { get; set; }
        public ICollection<ApplicationMessageFile> MessageFiles { get; set; }
        public ICollection<ApplicationTaskFile> TaskFiles { get; set; }
    }
}
