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
    public class TaskRepository : ITaskRepository
    {
        private readonly ILogger<TaskRepository> _logger;
        private readonly ApplicationDbContext _dbContext;

        public TaskRepository(ILogger<TaskRepository> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public ApplicationTask GetById(long taskId)
        {
            return _dbContext.Tasks.Where(e => e.Id == taskId).Include(e => e.Project).Include(e => e.Sprint).FirstOrDefault();
        }

        public Task<List<ApplicationTask>> GetAllAsync()
        {            
            return _dbContext.TaskUsers.Include(e => e.Task).Include(e => e.User).Select(e => e.Task).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationTask>> GetByUserIdAsync(string userId)
        {
            var result = new List<ApplicationTask>();
            if (string.IsNullOrEmpty(userId))
            {
                return result;
            }

            await _dbContext.TaskUsers.Where(e => e.UserId == userId).Select(e => e.Task).ToListAsync();
            return result;
        }
    }
}
