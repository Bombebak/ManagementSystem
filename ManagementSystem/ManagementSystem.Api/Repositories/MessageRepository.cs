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
    public class MessageRepository : IMessageRepository
    {
        private readonly ILogger<MessageRepository> _logger;
        private readonly ApplicationDbContext _dbContext;

        public MessageRepository(ILogger<MessageRepository> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ApplicationMessage>> GetAllAsync()
        {
            return await _dbContext.Messages.Include(e => e.User).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationMessage>> GetAllByTaskIdAsync(long taskId)
        {
            return await _dbContext.TaskMessages.Where(e => e.TaskId == taskId).
                Include(e => e.Message).ThenInclude(e => e.MessageFiles).ThenInclude(e => e.File).
                Include(e => e.Message).ThenInclude(e => e.TaskMessages).
                Select(e => e.Message).ToListAsync();
        }

        public async Task<ApplicationMessage> GetByIdAsync(long id)
        {
            return await _dbContext.Messages.Where(e => e.Id == id).Include(e => e.MessageFiles).ThenInclude(e => e.File).FirstOrDefaultAsync();
        }     

    }
}
