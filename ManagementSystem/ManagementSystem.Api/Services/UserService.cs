using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using ManagementSystem.Api.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Services
{
    public class UserService : IUserService
    {
        public List<ListItemDto<string>> GetAvailableUsers(List<ApplicationUser> allUsers, List<ApplicationUser> usersInTeam)
        {
            var result = new List<ListItemDto<string>>();

            foreach (var user in allUsers)
            {
                var item = new ListItemDto<string>();
                item.Label = user.Email;
                item.Value = user.Email;
                if (usersInTeam.Any(e => e.Id == user.Id))
                {
                    item.IsSelected = true;
                }
                result.Add(item);
            }

            return result;
        }
    }
}
