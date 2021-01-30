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
    public class ProjectRepository : IProjectRepository
    {
        private readonly ILogger<ProjectRepository> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ILogger<ProjectRepository> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task<ApplicationProject> GetByIdAsync(long id)
        {
            return _dbContext.Projects.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<List<ApplicationProject>> GetAllAsync()
        {
            return _dbContext.Projects.ToListAsync();
        }
    }
}
