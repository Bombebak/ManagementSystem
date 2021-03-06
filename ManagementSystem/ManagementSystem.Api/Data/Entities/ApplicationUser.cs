﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfileImagePath { get; set; }
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public virtual ICollection<ApplicationTeamUser> TeamUsers { get; set; }
        public virtual ICollection<ApplicationSprintUser> SprintUsers { get; set; }
        public virtual ICollection<ApplicationTaskUser> TaskUsers { get; set; }
        public virtual ICollection<ApplicationTimeRegistration> TimeRegistrations { get; set; }
        public virtual ICollection<ApplicationMessage> Messages { get; set; }
        public virtual ICollection<ApplicationFile> Files { get; set; }
    }
}
