using ManagementSystem.Api.Data;
using ManagementSystem.Api.Data.Entities;
using ManagementSystem.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ILogger<TeamRepository> _logger;
        private readonly ApplicationDbContext _dbContext;

        public TeamRepository(ILogger<TeamRepository> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<List<ApplicationTeam>> GetAllAsync()
        {
            return _dbContext.Teams.ToListAsync();
        }

        public Task<List<ApplicationTeam>> GetAllByUserIdAsync(string userId)
        {
            return _dbContext.TeamUsers.Where(e => e.UserId == userId).Select(e => e.Team).ToListAsync();
        }

        public Task<ApplicationTeam> GetByIdAsync(long id)
        {
            return _dbContext.Teams.Include(e => e.TeamUsers).ThenInclude(e => e.User).FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<List<ApplicationUser>> GetUsersInTeam(long teamId)
        {
            return _dbContext.TeamUsers.Where(e => e.TeamId == teamId).Select(e => e.User).ToListAsync();
        }

        public Task<List<ApplicationTeamUser>> GetTeamUsersInTeam(long teamId)
        {
            return _dbContext.TeamUsers.Where(e => e.TeamId == teamId).ToListAsync();
        }
    }
}
