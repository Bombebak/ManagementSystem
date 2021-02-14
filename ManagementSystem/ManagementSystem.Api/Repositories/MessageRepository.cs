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
            return await _dbContext.Messages.Include(e => e.Children).Include(e => e.User).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationMessage>> GetAllByTaskIdAsync(long taskId)
        {
            return await _dbContext.TaskMessages.Where(e => e.TaskId == taskId).
                Include(e => e.Message).ThenInclude(e => e.MessageFiles).ThenInclude(e => e.File).
                Include(e => e.Message).ThenInclude(e => e.Children).
                Include(e => e.Message).ThenInclude(e => e.TaskMessages).
                Select(e => e.Message).ToListAsync();
        }

        public async Task<ApplicationMessage> GetByIdAsync(long id)
        {
            return await _dbContext.Messages.Where(e => e.Id == id).Include(e => e.MessageFiles).ThenInclude(e => e.File).FirstOrDefaultAsync();
        }

        public async Task<ApplicationMessage> GetByIdIncludedFilesAndChildrenAsync(long id)
        {
            var messages = _dbContext.Messages.Where(e => e.Id == id);
            foreach (var msg in messages)
            {
                IncludeChildren(msg, true, true);
            }
            return await messages.FirstOrDefaultAsync();
        }

        public async Task<ApplicationMessage> GetByIdIncludedChildrenAsync(long id)
        {
            var messages = _dbContext.Messages.Where(e => e.Id == id);
            foreach (var msg in messages)
            {
                IncludeChildren(msg, false, false);
            }
            return await messages.FirstOrDefaultAsync();
        }

        private void IncludeChildren(ApplicationMessage msg, bool includeFiles, bool includeTaskMessages)
        {
            var entry = _dbContext.Entry(msg);
            entry.Collection(e => e.Children).Query().Load();
            if (includeFiles)
            {
                entry.Collection(e => e.MessageFiles).Query().Include(e => e.File).Load();
            }
            if (includeTaskMessages)
            {
                entry.Collection(e => e.TaskMessages).Query().Load();
            }
            if (msg.Children != null)
            {
                foreach (var child in msg.Children)
                {
                    IncludeChildren(child, includeFiles, includeTaskMessages);
                }
            }
        }

        public async Task<ApplicationMessage> GetByIdIncludedChildrenAndFilesAsync(long id)
        {
            return await _dbContext.Messages.Include(e => e.Children).Include(e => e.MessageFiles).ThenInclude(e => e.File).FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
