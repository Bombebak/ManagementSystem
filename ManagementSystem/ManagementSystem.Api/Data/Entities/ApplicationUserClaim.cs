using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
