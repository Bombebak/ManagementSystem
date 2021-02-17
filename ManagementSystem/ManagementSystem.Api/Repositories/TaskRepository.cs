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

        public async Task<ApplicationTask> GetByIdAsync(long taskId)
        {
            return await _dbContext.Tasks.Where(e => e.Id == taskId).
                Include(e => e.Project).Include(e => e.Sprint).
                Include(e => e.TaskUsers).ThenInclude(e => e.User).
                Include(e => e.TaskFiles).ThenInclude(e => e.File).
                FirstOrDefaultAsync();
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

            await _dbContext.TaskUsers.Where(e => e.UserId == userId).Include(e => e.User).Select(e => e.Task).ToListAsync();
            return result;
        }

        public async Task<List<ApplicationTask>> GetAllWithoutToList(string searchText, long? projectId, long? sprintId, List<string> userIds)
        {
            IQueryable<ApplicationTask> tasks = _dbContext.Tasks;
            if (userIds.Any())
            {
                tasks = tasks.Include(e => e.TaskUsers);
                foreach (var item in userIds)
                {
                    tasks = tasks.Where(e => e.TaskUsers.Any(ee => ee.UserId == item));
                }
            }
            if (projectId.GetValueOrDefault() != 0)
            {
                tasks = tasks.Where(e => e.ProjectId.HasValue && e.ProjectId.Value == projectId.Value);
            }
            if (sprintId.GetValueOrDefault() != 0)
            {
                tasks = tasks.Where(e => e.SprintId.HasValue && e.SprintId.Value == sprintId.Value);
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                //https://nugetmusthaves.com/Package/Lucene.Net
                //taskUsers = taskUsers.Where(e => e.Task.Name.Contains(searchText));
            }

            tasks = tasks.Include(e => e.Project).Include(e => e.Sprint).Include(e => e.TaskUsers).ThenInclude(e => e.User);
            return await tasks.ToListAsync();
        }
    }
}
