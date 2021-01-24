using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
