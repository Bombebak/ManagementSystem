using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IUserService
    {
        List<ListItemDto<string>> GetAvailableUsers(List<ApplicationUser> allUsers, List<ApplicationUser> usersInTeam);
    }
}
