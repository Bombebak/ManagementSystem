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
    public class SprintRepository : ISprintRepository
    {
        private readonly ILogger<SprintRepository> _logger;
        private readonly ApplicationDbContext _dbContext;

        public SprintRepository(ILogger<SprintRepository> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<List<ApplicationSprint>> GetAllAsync()
        {
            return _dbContext.Sprints.ToListAsync();
        }        
    }
}
