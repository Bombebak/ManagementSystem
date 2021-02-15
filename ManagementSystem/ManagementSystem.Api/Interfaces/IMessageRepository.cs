using ManagementSystem.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Api.Interfaces
{
    public interface IMessageRepository
    {
        Task<ApplicationMessage> GetByIdAsync(long id);
        Task<IEnumerable<ApplicationMessage>> GetAllAsync();
        Task<IEnumerable<ApplicationMessage>> GetAllByTaskIdAsync(long taskId);

    }
}
