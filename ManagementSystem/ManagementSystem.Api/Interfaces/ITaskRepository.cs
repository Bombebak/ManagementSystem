using ManagementSystem.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface ITaskRepository
    {
        ApplicationTask GetById(long taskId);
        Task<List<ApplicationTask>> GetAllAsync();
        Task<IEnumerable<ApplicationTask>> GetByUserIdAsync(string userId);
        Task<List<ApplicationTask>> GetAllWithoutToList(string searchText, long? projectId, long? sprintId, List<string> userIds);
    }
}
