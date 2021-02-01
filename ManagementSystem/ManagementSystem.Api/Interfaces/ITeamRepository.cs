using ManagementSystem.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<ApplicationTeam>> GetAllAsync();
        Task<List<ApplicationTeam>> GetAllByUserIdAsync(string userId);
        Task<ApplicationTeam> GetByIdAsync(long id);
        Task<List<ApplicationUser>> GetUsersInTeam(long teamId);
        Task<List<ApplicationTeamUser>> GetTeamUsersInTeam(long teamId);
    }
}
